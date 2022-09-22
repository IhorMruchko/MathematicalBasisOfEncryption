namespace EncryptionWPF.Services.MessageFomratingService;

public class FrequencyData
{
    public char Letter { get; set; }

    public int Amount { get; set; }

    public int Capacity { get; set; }

    public float Frequency => Amount / (float)Capacity;
}
