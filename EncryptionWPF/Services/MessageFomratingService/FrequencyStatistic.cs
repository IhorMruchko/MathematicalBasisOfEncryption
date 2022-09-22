using MathematicalBasisOfEncryption.CipherBase;
using System.Collections.Generic;
using System.Linq;

namespace EncryptionWPF.Services.MessageFomratingService;

public class FrequencyStatistic
{
    public static IEnumerable<FrequencyData> GetFrequencyForMessage(string message, CodingAlphabet alphabet)
    {
        return alphabet?.Alphabet.Select(v => new FrequencyData()
        {
            Letter = char.ToLower(v),
            Amount = message.Count(c => char.ToLower(c).Equals(char.ToLower(v))),
            Capacity = message.Length
        });
    }
}
