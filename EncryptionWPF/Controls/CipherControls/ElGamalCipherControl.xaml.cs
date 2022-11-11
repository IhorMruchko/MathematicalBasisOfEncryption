using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using MathematicalBasisOfEncryption.Extensions;
using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace EncryptionWPF.Controls.CipherControls;

public partial class ElGamalCipherControl : UserControl, ICipherUserControl
{
    private ElGamal _cipher = new ();

    private Regex _charFormatValidator = new("[1-9]+");

    public ElGamalCipherControl()
    {
        InitializeComponent();
    }

    public Cipher Cipher => _cipher.SetKey(GetKey());

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
        e.Handled = !(_charFormatValidator?.IsMatch(e.Text) ?? true);
    }

    private (long, long, long, long, long) GetKey()
    {
        return (PValueSelector.Text.ToLong(), 
            QValueSelector.Text.ToLong(), 
            PRSAValueSelector.Text.ToLong(),
            QRSAValueSelector.Text.ToLong(),
            EValueSelector.Text.ToLong());
    }

}
