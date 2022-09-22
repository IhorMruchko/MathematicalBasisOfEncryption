using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EncryptionWPF.Controls;
using EncryptionWPF.Controls.CipherControls;
using EncryptionWPF.Controls.FormatControls;

namespace EncryptionWPF;

public partial class MainWindow
{
    private static readonly Dictionary<string, IFormatUserControl> _formatContentChanger = new ()
    {
        ["Text"] = new TextUserControl(),
        ["Picture"] = new ImageUserControl(),
    };

    private static readonly Dictionary<string, ICipherUserControl> _cipherContentChanger = new ()
    {
        ["Ceasar Cipher"] = new CaesarCipherUserControl(),
        ["Trithemius Cipher"] = new TrithemiusCipherControl(),
        ["Gamma Cipher"] = new GammaCipherControl(),
        ["Vigenere Cipher"] = new VigenereCipherControl(),
        ["RSA Cipher"] = new RSACipherControl(),
        ["Diffi-Helman"] = new DiffiHelmanUserControl(),
        ["Playfair Cipher"] = new PlayfairCipherUserControl()
    };

    public MainWindow()
    {
        InitializeComponent();
    }

    public static IEnumerable<string> InitialFormatsSelected => _formatContentChanger.Keys;

    public static IEnumerable<string> InitialCiphersSelected => _cipherContentChanger.Keys;

    public ICipherUserControl CurrentCipherControl => _cipherContentChanger[CurrentCipherValue];

    public IFormatUserControl CurrentFormat => _formatContentChanger[CurrentFormatValue ?? "Text"];

    private string CurrentCipherValue => _cipherContentSelector.SelectedItem as string;

    private string CurrentFormatValue => _formatSelector?.SelectedItem as string;

    private void FormatSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _formatContent.Content = CurrentFormat;
        CurrentCipherControl.SetFormatControl(CurrentFormat);
        CurrentFormat.SetCypherControl(CurrentCipherControl);
    }

    private void CipherContentSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _cipherContent.Content = CurrentCipherControl;
        CurrentCipherControl.SetFormatControl(CurrentFormat);
        CurrentFormat.SetCypherControl(CurrentCipherControl);
    }

    private void OpenFile(object sender, RoutedEventArgs e)
    {
        CurrentFormat.OpenFile();
    }

    private void SaveFile(object sender, RoutedEventArgs e)
    {
        CurrentFormat.SaveFile();
    }

    private void DecodeButton_Click(object sender, RoutedEventArgs e)
    {
        CurrentFormat.DecodeButtonClick();
    }

    private void EncodeButton_Click(object sender, RoutedEventArgs e)
    {
        CurrentFormat.EncodeButtonClick();
    }
}