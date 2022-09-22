using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres;

namespace EncryptionWPF.Controls.CipherControls;

public partial class DiffiHelmanUserControl : UserControl, ICipherUserControl
{
    private readonly DiffieHellmanCipher _diffieHellmanCipher = new ();

    private readonly Random _generator = new ();

    public Cipher Cipher => _diffieHellmanCipher.SetKey(((long)PValueSlider.Value,
        (long)QValueSlider.Value,
        (long)(AValueSlider.Value == 0 ? _generator.Next(1, (int)PValueSlider.Value) : AValueSlider.Value),
        (long)(BValueSlider.Value == 0 ? _generator.Next(1, (int)PValueSlider.Value) : BValueSlider.Value)));

    public DiffiHelmanUserControl()
    {
        InitializeComponent();
    }

    public void SetFormatControl(IFormatUserControl formatControl) { }
    
    public void OnEncodeEventHandler()
    {
        MessageBox.Show($"Generated secret key: {_diffieHellmanCipher.SecretKey}");
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

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        AValueSlider.Value = _generator.Next(1, (int)PValueSlider.Value);
        BValueSlider.Value = _generator.Next(1, (int)PValueSlider.Value);
    }
}
