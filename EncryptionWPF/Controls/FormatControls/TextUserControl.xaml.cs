using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using EncryptionWPF.Services.IOService;
using EncryptionWPF.Services.MessageFomratingService;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.CipherBase.Alphabets;

namespace EncryptionWPF.Controls.FormatControls;

public partial class TextUserControl : UserControl, IFormatUserControl
{
    private const string INITIAL_TEXT_DIRECTORY
        = @"E:\Programming\C#\MathematicalBasisOfEncryption\EncryptionWPF\Resourse\Text";

    private const string FORCE_DECODER_NOT_FOUND_ERROR = "There is no force decoder for this cipher";

    private const string SAVE_TEXT_FILE_TITLE = "Save text data";

    private const string OPEN_TEXT_FILE_TITLE = "Open text file to encrypt";

    private static readonly Dictionary<string, CodingAlphabet> _languageChanger = new ()
    {
        ["Ukrainian"] = new UkraineCodingAlphabet(),
        ["English"] = new EnglishCodingAlphabet()
    };

    private ICipherUserControl _currentCypherControl;

    public TextUserControl()
    {
        InitializeComponent();
    }

    public static IEnumerable<string> LanguageValues => _languageChanger.Keys;

    public CodingAlphabet CurrentCodingAlphabet
        => _languageChanger[_languageSelector.SelectedItem as string];

    private CipherForceDecoder ForceDecoder => _currentCypherControl.Cipher.SetAlphabet(CurrentCodingAlphabet).ForceDecoder;

    public void SetCypherControl(ICipherUserControl cypherControl)
    {
        _currentCypherControl = cypherControl;
    }

    public void DecodeButtonClick()
    {
        _currentCypherControl.Cipher.SetAlphabet(CurrentCodingAlphabet);
        TextContainer.Text = _currentCypherControl.Cipher.Decode(TextContainer.Text);
        _currentCypherControl.OnDecodeEventHandler();
    }

    public void EncodeButtonClick()
    {
        _currentCypherControl.Cipher.SetAlphabet(CurrentCodingAlphabet);
        TextContainer.Text = _currentCypherControl.Cipher.Encode(TextContainer.Text);
        _currentCypherControl.OnEncodeEventHandler();
    }

    public void SaveFile()
    {
        var saveFileDialog = new DialogBuilder()
            .SetFormat(DialogFormat.Text)
            .SetInitialDirectory(INITIAL_TEXT_DIRECTORY)
            .SetTitle(SAVE_TEXT_FILE_TITLE)
            .CreateSaveDialog();

        if (saveFileDialog.ShowDialog() == true)
        {
            File.WriteAllText(saveFileDialog.FileName, TextContainer.Text);
        }
    }

    public void OpenFile()
    {
        var openFileDialog = new DialogBuilder()
            .SetFormat(DialogFormat.Text)
            .SetInitialDirectory(INITIAL_TEXT_DIRECTORY)
            .SetTitle(OPEN_TEXT_FILE_TITLE)
            .CreateOpenDialog();

        if (openFileDialog.ShowDialog() == true)
        {
            TextContainer.Text = File.ReadAllText(openFileDialog.FileName);
        }
    }

    private void FrequencyTab_Selected(object sender, System.Windows.RoutedEventArgs e)
    {
        _frequencyTable.ItemsSource = FrequencyStatistic.GetFrequencyForMessage(
            TextContainer.Text,
            CurrentCodingAlphabet);
    }

    private void ForceDecodeButton_Cick(object sender, System.Windows.RoutedEventArgs e)
    {
        if (ForceDecoder == null)
        {
            ForceDecodingResult.Text = FORCE_DECODER_NOT_FOUND_ERROR;
            return;
        }

        ForceDecodingResult.Text = ForceDecoder.ForceDecode(MessageText.Text, EncryptedMessageText.Text);
    }
}
