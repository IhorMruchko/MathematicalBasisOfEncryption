using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using System.Windows.Controls;

namespace EncryptionWPF.Controls.CipherControls
{
    public partial class GammaCipherControl : UserControl, ICipherUserControl
    {
        private GammaCipher _gammaCipher = new();

        private IFormatUserControl _formatUserControl;
        
        public Cipher Cipher => _gammaCipher.SetKey(IsNeedToRegenerateKey.IsChecked);

        public GammaCipherControl()
        {
            InitializeComponent();
        }

        public void OnDecodeEventHandler() { }

        public void OnEncodeEventHandler()
        {
            GammaKeyTextBox.Text = _gammaCipher.Gamma;
        }

        public void SetFormatControl(IFormatUserControl formatControl)
        {
            _formatUserControl = formatControl;
        }
    }
}
