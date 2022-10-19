using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using System.Windows.Controls;

namespace EncryptionWPF.Controls.CipherControls;

public partial class RС6CipherControl : UserControl, ICipherUserControl
{
    public Cipher Cipher => new RC6().SetKey(KeyValueSelector.Text);

    public RС6CipherControl()
    {
        InitializeComponent();
    }

    public void SetFormatControl(IFormatUserControl formatControl)
    {
        
    }

    public void OnEncodeEventHandler()
    {
        
    }

    public void OnDecodeEventHandler()
    {
        
    }
}
