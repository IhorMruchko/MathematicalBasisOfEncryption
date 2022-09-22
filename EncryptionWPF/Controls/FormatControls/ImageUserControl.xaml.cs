using System.Windows.Controls;
using EncryptionWPF.Controls.FormatControls.PictureStates;

namespace EncryptionWPF.Controls.FormatControls;

public partial class ImageUserControl : UserControl, IFormatUserControl
{
    public string Filepath { get; set; }

    public ICipherUserControl CurrentCypherUserControl { get; set; }
    
    public IImageUserControlState CurrentState { get; set; } = new ImageTabOpenState();

    public ImageUserControl()
    {
        InitializeComponent();
    }

    public void EncodeButtonClick()
    {
        CurrentState.Encode(this);
        CurrentCypherUserControl.OnEncodeEventHandler();
    }
    
    public void DecodeButtonClick()
    {
        CurrentState.Decode(this);
        CurrentCypherUserControl.OnDecodeEventHandler();
    }

    public void OpenFile() => CurrentState.OpenFile(this);

    public void SaveFile() => CurrentState.SaveFile(this);

    public void SetState(IImageUserControlState state) => CurrentState = state;
    
    public void SetCypherControl(ICipherUserControl cypherControl) 
        => CurrentCypherUserControl = cypherControl;

    private void ConvrtedTextTabItem_Selected(object sender, System.Windows.RoutedEventArgs e) 
        => CurrentState.SelectTextTab(this);

    private void ImageTabItem_Selected(object sender, System.Windows.RoutedEventArgs e) 
        => CurrentState.SelectImageTab(this);
}
