using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace EncryptionWPF.Controls.CipherControls;

public partial class PlayfairCipherUserControl : UserControl, ICipherUserControl
{
    private PlayfairCipher _cipher = new();
    
    private Regex charFormatValidator = new("[1-9]+");
   
    public PlayfairCipherUserControl()
    {
        InitializeComponent();
    }

    public Cipher Cipher => _cipher.SetKey(KeyValueSelector.Text);

    public void OnDecodeEventHandler() { }
    
    public void OnEncodeEventHandler() 
    {
        MessageBox.Show(_cipher.ToString(), "Key table", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void SetFormatControl(IFormatUserControl formatControl) { }

    private void KeyValueSelector_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = (charFormatValidator?.IsMatch(e.Text) ?? true);
    }
}
