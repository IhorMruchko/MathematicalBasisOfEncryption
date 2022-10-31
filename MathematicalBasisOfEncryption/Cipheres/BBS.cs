using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Extensions;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static System.Math;


namespace MathematicalBasisOfEncryption.Cipheres;

public class BBS : Cipher
{
    public const int CHAR_BIT_SIZE = 16;
    private long _p;
    private long _q;
    private long _r;
    private long _startValue;
    private long _currentValue;

    private Random _randomizer = new ();

    public long N => _p * _q;
    public long Modulus => (_p - 1) * (_q - 1);

    public override CipherForceDecoder ForceDecoder => null;

    public override string Decode(string message)
    {
        var bytesRepr = Encoding.Unicode.GetBytes(message);
        var generatedBytes = new byte[bytesRepr.Length];
        new BitArray(Enumerable.Range(0, bytesRepr.Length * 8)
                               .Select(_ => Generate())
                               .ToArray())
            .CopyTo(generatedBytes, 0);

        return Encoding.Unicode.GetString(bytesRepr.Zip(generatedBytes)
                                                   .Select(z => (byte)(z.First ^ z.Second))
                                                   .ToArray());

    }

    public override string Encode(string message)
    {
        var bytesRepr = Encoding.Unicode.GetBytes(message);
        var generatedBytes = new byte[bytesRepr.Length];
        new BitArray(Enumerable.Range(0, bytesRepr.Length * 8)
                               .Select(_ => Generate())
                               .ToArray())
            .CopyTo(generatedBytes, 0);

        return Encoding.Unicode.GetString(bytesRepr.Zip(generatedBytes)
                                                   .Select(z => (byte)(z.First ^ z.Second))
                                                   .ToArray());
    }

    public override bool ValidateKey(object key)
    {
        if (key is (long, long))
        {
            var (p, q) = ((long, long))key;
            return p.IsPrime() && q.IsPrime() && p % 4 == 3 && q % 4 == 3;
        }
        return false;
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
        (_p, _q) = ((long, long))key;
        SelectR();
        return this;
    }

    protected override Cipher SetDefaultKey()
    {
        (_p, _q) = (13_163, 92_627);
        SelectR();
        return this;
    }

    private void SelectR()
    {
        while (_r.IsCoprime(N) == false)
        {
            _r = _randomizer.Next(2, (int)N);
        }
        _startValue = (long)(Pow(_r, 2) % N);
        _currentValue = _startValue;
    }

    private bool Generate()
    {
        _currentValue = (long)Pow(_currentValue, 2) % N;
        return Pow(_currentValue, 2) % 2 == 1;
    }
}
