using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using MathematicalBasisOfEncryption.Extensions;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EncryptionWPF.Controls.CipherControls;

public partial class BBSCipherControl : ICipherUserControl
{
    private readonly BBS _cipher = new();
    
    private Regex _charFormatValidator = new("[1-9]+");

    public Cipher Cipher => _cipher.SetKey(GetKey());

    public BBSCipherControl()
    {
        InitializeComponent();
    }

    public void OnDecodeEventHandler()
    {
        Debug.WriteLine(_cipher.N.ToString());
    }

    public void OnEncodeEventHandler()
    {

    }

    public void SetFormatControl(IFormatUserControl formatControl)
    {

    }

    private void KeyValueSelector_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !(e.Text == "-" || (_charFormatValidator?.IsMatch(e.Text) ?? true));
    }

    private (long, long) GetKey()
    {
        if (PKeyValueSelector.Text == string.Empty || QKeyValueSelector.Text == string.Empty)
        {
            return (2, 2);
        }

        var pValue = long.Parse(PKeyValueSelector.Text);
        var qValue = long.Parse(QKeyValueSelector.Text);

        pValue.ChangeTo(v => v.IsPrime() && v % 4 == 3, v => ++v);
        qValue.ChangeTo(v => v.IsPrime() && v % 4 == 3, v => ++v);

        return (pValue, qValue);
    }
}
