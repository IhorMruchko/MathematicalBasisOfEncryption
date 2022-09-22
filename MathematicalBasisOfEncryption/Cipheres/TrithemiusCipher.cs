using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres.CipheresValidators.TrithemiusCipherKeyConveters;
using MathematicalBasisOfEncryption.CipherForceDecoders;

namespace MathematicalBasisOfEncryption.Cipheres;

public class TrithemiusCipher : Cipher
{
    private List<IKeyToIntConverter> _converters = new ()
    {
            new From2DVectorToInt(),
            new From3DVectorToInt(),
            new FromStringToInt(null)
    };

    private IKeyToIntConverter _selectedConverter;

    private int _currentPositionInText;

    public override CipherForceDecoder ForceDecoder => new TrithemiusCipherForceDecoder().SetCipher(this);

    public override string Encode(string message)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < message.Length; ++i)
        {
            _currentPositionInText = i % CodingAlphabet.Power;
            builder.Append(DefineCase(message[i], GetEncodedLetter));
        }

        return builder.ToString();
    }

    public override string Decode(string message)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < message.Length; ++i)
        {
            _currentPositionInText = i % CodingAlphabet.Power;
            builder.Append(DefineCase(message[i], GetDecodedLetter));
        }

        return builder.ToString();
    }

    public override Cipher SetAlphabet(CodingAlphabet alphabet)
    {
        base.SetAlphabet(alphabet);
        _converters = new ()
        {
            new From2DVectorToInt(),
            new From3DVectorToInt(),
            new FromStringToInt(this)
        };
        return this;
    }

    public override bool ValidateKey(object key)
    {
        _selectedConverter = _converters.FirstOrDefault(x => x.IsAbleToConvert(key));
        return _selectedConverter != null;
    }

    protected override char GetEncodedLetter(char letter)
    {
        var positionInAlphabet = CodingAlphabet[letter];
        return CodingAlphabet[positionInAlphabet + _selectedConverter.GetShift(_currentPositionInText)];
    }

    protected override char GetDecodedLetter(char letter)
    {
        var positionInAlphabet = CodingAlphabet[letter];
        return CodingAlphabet[positionInAlphabet - _selectedConverter.GetShift(_currentPositionInText)];
    }

    protected override Cipher SetDefaultKey()
    {
        _selectedConverter = new From2DVectorToInt();
        _selectedConverter.IsAbleToConvert((1, 2));
        return this;
    }

    protected override Cipher SetCurrentKey(object key)
    {
        ValidateKey(key);
        return this;
    }
}
