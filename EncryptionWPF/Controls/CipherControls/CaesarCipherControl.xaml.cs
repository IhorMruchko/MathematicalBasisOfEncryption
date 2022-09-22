using System.Windows.Controls;
using System.Text.RegularExpressions;
using MathematicalBasisOfEncryption.Cipheres;
using MathematicalBasisOfEncryption.CipherBase;

namespace EncryptionWPF.Controls.CipherControls;

public partial class CaesarCipherUserControl : UserControl, ICipherUserControl
{
    private const int DEFAULT_VALUE = 3;
    
    private Regex charFormatValidator = new ("[1-9]+");

    protected CeasarCipher _ceasarCipher = new ();

    public Cipher Cipher => _ceasarCipher.SetKey(int.TryParse(KeyValueSelector.Text, out var input) ? input : DEFAULT_VALUE);

    public CaesarCipherUserControl()
    {
        InitializeComponent();
    }

    public void SetFormatControl(IFormatUserControl formatControl) { }

    public void OnEncodeEventHandler() { }

    public void OnDecodeEventHandler() { }

    private void KeyValueSelector_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !(charFormatValidator?.IsMatch(e.Text) ?? true);
    }
}
