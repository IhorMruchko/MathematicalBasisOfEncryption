using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using System.Diagnostics;

namespace EncryptionWPF.Controls.CipherControls;

public partial class BBSCipherControl : ICipherUserControl
{
    private BBS _cipher = new();
    public Cipher Cipher => _cipher.SetKey((1, 2));

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
}
