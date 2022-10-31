using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using System.Windows.Controls;

namespace EncryptionWPF.Controls.CipherControls;

public partial class LFSRCipherControl : UserControl, ICipherUserControl
{
    private readonly LFSR _cipher = new();
    private readonly string _acceptableLetters = "01";
    
    public LFSRCipherControl()
    {
        InitializeComponent();
    }

    public Cipher Cipher => _cipher.SetKey(KeyValueSelector.Text);

    public void OnDecodeEventHandler()
    {
    }

    public void OnEncodeEventHandler()
    {
    }

    public void SetFormatControl(IFormatUserControl formatControl)
    {
    }

    private void KeyValueSelector_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !(_acceptableLetters.Contains(e.Text) && KeyValueSelector.Text.Length - 1 < 10);
    }
}
