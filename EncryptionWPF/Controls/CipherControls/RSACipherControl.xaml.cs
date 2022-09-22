using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;

namespace EncryptionWPF.Controls.CipherControls;

public partial class RSACipherControl : UserControl, ICipherUserControl
{
    private readonly RSACipher _cipher = new();

    public RSACipherControl()
    {
        InitializeComponent();
    }

    public Cipher Cipher => _cipher.SetKey(((int)PValueSlider.Value, (int)QValueSlider.Value, (int)EValueSlider.Value));

    public void SetFormatControl(IFormatUserControl formatControl) { }

    public void OnEncodeEventHandler()
    {
        MessageBox.Show((_cipher.ValidationMessage is null ? "" : $"Key was set to default due to: \"{_cipher.ValidationMessage}\"\n") +
            $"Public key: n = {_cipher.N}; e = {_cipher.EValue};" +
            $"\nSecred key: d = {_cipher.D}", "Your keys", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void OnDecodeEventHandler() { }

    private void ValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        var slider = (Slider)sender;
        slider.Value = (int)slider.Value;
    }

    private void ValueSlider_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ((Slider)sender).Value = 0;
    }

    private void ValueSlider_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        ((Slider)sender).Value += e.Delta > 0 ? 1 : -1;
    }
}
