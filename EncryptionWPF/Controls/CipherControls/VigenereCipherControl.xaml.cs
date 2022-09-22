using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using System.Windows.Controls;

namespace EncryptionWPF.Controls.CipherControls;

public partial class VigenereCipherControl : UserControl, ICipherUserControl
{
    private VigenеreCipher _vigenereCipher = new();
    
    public VigenereCipherControl()
    {
        InitializeComponent();
    }

    public Cipher Cipher => _vigenereCipher.SetKey(KeyValueInputTextBox.Text);

    public void OnEncodeEventHandler() { }

    public void OnDecodeEventHandler() { }

    public void SetFormatControl(IFormatUserControl formatControl) { }
}
