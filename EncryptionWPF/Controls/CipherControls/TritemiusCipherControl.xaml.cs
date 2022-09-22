using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EncryptionWPF.Controls.CipherControls;

public partial class TrithemiusCipherControl : UserControl, ICipherUserControl
{
    private TrithemiusCipher _trithemiusCipher = new();

    public Cipher Cipher => _trithemiusCipher.SetKey(GetKey());

    public TrithemiusCipherControl()
    {
        InitializeComponent();
    }

    public void SetFormatControl(IFormatUserControl formatControl) { }

    public void OnEncodeEventHandler() { }

    public void OnDecodeEventHandler() { }

    private object GetKey()
    {
        return MottoSetter.Text == string.Empty
            ? ((int)AValueSlider.Value, (int)BValueSlider.Value, (int)CValueSlider.Value)
            : MottoSetter.Text;
    }

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
