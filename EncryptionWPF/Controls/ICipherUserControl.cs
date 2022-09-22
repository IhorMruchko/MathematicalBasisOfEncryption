using MathematicalBasisOfEncryption.CipherBase;

namespace EncryptionWPF.Controls;

public interface ICipherUserControl
{
    public Cipher Cipher { get; }

    public void SetFormatControl(IFormatUserControl formatControl);

    public abstract void OnEncodeEventHandler();

    public abstract void OnDecodeEventHandler();
}
