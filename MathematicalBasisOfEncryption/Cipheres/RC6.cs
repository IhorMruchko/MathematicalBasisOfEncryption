using MathematicalBasisOfEncryption.CipherBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MathematicalBasisOfEncryption.Cipheres;

public class RC6 : Cipher
{
    public const int WORD_SIZE = 32;
    public const int BYTES_PER_WORD = WORD_SIZE / 8;
    public const int BLOCK_SIZE = 4 * BYTES_PER_WORD;
    public const uint ROUNDS = 20;
    public const uint PW = 0xB7E15163;
    public const uint QW = 0x9e3779b9;
    public const uint LGW = 5;

    private static byte[] _defaultKey = Encoding.Unicode.GetBytes("default key");
    private uint[] _keys;

    public override CipherForceDecoder ForceDecoder => null;

    public override string Decode(string message)
    {
        if (TryParseMessage(message, out var bytes) == false)
        {
            return message;
        }        
        var amountToEnd = bytes.Length % BLOCK_SIZE;
        bytes = bytes.Concat(Enumerable.Range(0, amountToEnd).Select(_ => (byte)0)).ToArray();
        var blocksAmount = bytes.Length / BLOCK_SIZE;
        return Encoding.Unicode.GetString(Enumerable.Range(0, blocksAmount)
            .SelectMany(i => DecodeBlock(bytes, i * BLOCK_SIZE)).ToArray());
    }

    private static bool TryParseMessage(string message, out byte[] result)
    {   
        var bytes = message.Split(' ');
        result = new byte[bytes.Length];
        foreach(var (index, value) in Enumerable.Range(0, bytes.Length).Zip(bytes))
        {
            if (byte.TryParse(value, out var intValue) == false)
            {
                return false;
            }
            result[index] = intValue;
        }

        return true;
    }

    public override string Encode(string message)
    {
        var bytes = Encoding.Unicode.GetBytes(message);
        var amountToEnd = bytes.Length % BLOCK_SIZE;
        bytes = bytes.Concat(Enumerable.Range(0, amountToEnd).Select(_ => (byte)0)).ToArray();
        var blocksAmount = bytes.Length / BLOCK_SIZE;
        return string.Join(' ', Enumerable.Range(0, blocksAmount)
            .SelectMany(i => EncodeBlock(bytes, i * BLOCK_SIZE)).ToArray());
    }

    public override bool ValidateKey(object key)
    {
        return key is string k && k.Trim() != string.Empty;
    }

    protected override char GetDecodedLetter(char letter)
    {
        throw new System.NotImplementedException();
    }

    protected override char GetEncodedLetter(char letter)
    {
        throw new System.NotImplementedException();
    }

    protected override Cipher SetCurrentKey(object key)
    {
        BuildKey(Encoding.UTF8.GetBytes(key as string));
        return this;
    }

    protected override Cipher SetDefaultKey()
    {
        BuildKey(_defaultKey);
        return this;
    }

    private void BuildKey(byte[] key)
    {
        var digitalWordCapacity = (key.Length + (BYTES_PER_WORD - 1)) / BYTES_PER_WORD;
        var keyCopy = new uint[digitalWordCapacity];
        for (var i = key.Length - 1; i>= 0; --i)
        {
            keyCopy[i / BYTES_PER_WORD] = (uint)((keyCopy[i / BYTES_PER_WORD] << 8) + (key[i] & 0xff));
        }

        _keys = new uint[2 + 2 * ROUNDS + 2];
        _keys[0] = PW;
        for (var i = 1; i < _keys.Length; ++i)
        {
            _keys[i] = _keys[i - 1] + QW;
        }

        var iterationAmount = keyCopy.Length > _keys.Length
             ? 3 * keyCopy.Length
             : 3 * _keys.Length;

        uint A = 0, B = 0, ii = 0, jj = 0;
        
        for(var i=0; i < iterationAmount; ++i)
        {
            A = _keys[ii] = RotateLeft(_keys[ii] + A + B, 3);
            B = keyCopy[jj] = RotateLeft(keyCopy[jj] + A + B, A + B);
            ii = (uint)((ii + 1) % _keys.Length);
            jj = (uint)((jj + 1) % keyCopy.Length);
        }
    }

    private byte[] EncodeBlock(byte[] input, int blockStart)
    {
        var result = new List<byte>(BLOCK_SIZE);
        var A = BytesToWord(input, blockStart);
        var B = BytesToWord(input, blockStart + BYTES_PER_WORD);
        var C = BytesToWord(input, blockStart + BYTES_PER_WORD * 2);
        var D = BytesToWord(input, blockStart + BYTES_PER_WORD * 3);

        B += _keys[0];
        D += _keys[1];

        for(var i = 1; i <= ROUNDS; i++)
        {
            
            uint t = B * (2 * B + 1);
            t = RotateLeft(t, LGW);

            uint u = D * (2 * D + 1);
            u = RotateLeft(u, LGW);

            A ^= t;
            A = RotateLeft(A, u);
            A += _keys[2 * i + 1];

            C ^= u;
            C = RotateLeft(C, t);
            C += _keys[2 * i + 1];

            (A, B, C, D) = (B, C, D, A);
        }

        A += _keys[2 * ROUNDS + 2];
        C += _keys[2 * ROUNDS + 3];


        result.AddRange(WordToBytes(A));
        result.AddRange(WordToBytes(B));
        result.AddRange(WordToBytes(C));
        result.AddRange(WordToBytes(D));
        return result.ToArray();
    }

    private byte[] DecodeBlock(byte[] input, int blockStart)
    {
        var result = new List<byte>(BLOCK_SIZE);
        var A = BytesToWord(input, blockStart);
        var B = BytesToWord(input, blockStart + BYTES_PER_WORD);
        var C = BytesToWord(input, blockStart + BYTES_PER_WORD * 2);
        var D = BytesToWord(input, blockStart + BYTES_PER_WORD * 3);

        C -= _keys[2 * ROUNDS + 3];
        A -= _keys[2 * ROUNDS + 2];

        for (var i = ROUNDS; i >= 1; --i)
        {
            (D, C, B, A) = (C, B, A, D);

            var t = B * (2 * B + 1);
            t = RotateLeft(t, LGW);

            var u = D * (2 * D + 1);
            u = RotateLeft(u, LGW);

            C -= _keys[2 * i + 1];
            C = RotateRight(C, t);
            C ^= u;

            A -= _keys[2 * i + 1];
            A = RotateRight(A, u);
            A ^= t;
        }

        D -= _keys[1];
        B -= _keys[0];

        result.AddRange(WordToBytes(A));
        result.AddRange(WordToBytes(B));
        result.AddRange(WordToBytes(C));
        result.AddRange(WordToBytes(D));

        return result.ToArray();
    }

    private static uint RotateLeft(uint x, uint y)
    {
        return (x << (int)(y & (WORD_SIZE - 1)))
                | (x >> (int)(WORD_SIZE - (y & (WORD_SIZE - 1))));
    }

    private static uint RotateRight(uint x, uint y)
    {
        return (x >> (int)(y & (WORD_SIZE - 1)))
            | (x << (int)(WORD_SIZE - (y & (WORD_SIZE - 1))));
    }

    private static byte[] WordToBytes(uint word)
    {
        var result = new byte[BYTES_PER_WORD];
        for(int i = 0; i < result.Length; ++i)
        {
            result[i] = (byte)word;
            word >>= 8;
        }

        return result;
    }

    private static uint BytesToWord(byte[] source, int sourceOffset)
    {
        uint result = 0;
        for (var i = BYTES_PER_WORD - 1; i >= 0; --i)
        {
            result = (uint)((result << 8) + (source[i + sourceOffset] & 0xff));
        }

        return result;
    }
} 
