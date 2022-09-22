namespace EncryptionWPF.Controls.FormatControls.PictureStates;

public interface IImageUserControlState
{
    public void Encode(ImageUserControl imageControl);
 
    public void Decode(ImageUserControl imageControl);

    public void OpenFile(ImageUserControl imageControl);

    public void SaveFile(ImageUserControl imageControl);

    public void SelectTextTab(ImageUserControl imageControl);
    
    public void SelectImageTab(ImageUserControl imageControl);
}