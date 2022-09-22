using EncryptionWPF.Services.IOService;
using static System.IO.File;

namespace EncryptionWPF.Controls.FormatControls.PictureStates;

public class TextTabOpenState : ImageUserControlTools, IImageUserControlState
{
    public virtual void Decode(ImageUserControl imageControl) 
        => FileIsNotLoadedException();

    public virtual void Encode(ImageUserControl imageControl) 
        => FileIsNotLoadedException();

    public virtual void SaveFile(ImageUserControl context)
        => FileIsNotLoadedException();

    public void OpenFile(ImageUserControl imageControl)
    {
        var dialog = new DialogBuilder()
            .SetTitle("Open base 64 version of the image")
            .SetFormat(DialogFormat.Text)
            .SetInitialDirectory(INITIAL_IMAGE_ENCODED_DIRECTORY)
            .CreateOpenDialog();

        if (dialog.ShowDialog() == true)
        {
            imageControl.TextImageContainer.Text = ReadAllText(dialog.FileName);
        }

        imageControl.SetState(new TextLoadedTextTabOpenState());
    }

    public void SelectImageTab(ImageUserControl imageControl)
    {
        var isAbleToLoadImage = ToImageFromBase64(imageControl, out var image);
        imageControl.ImageContainer.Source = image;
        imageControl.SetState(
                isAbleToLoadImage 
                ? new ImageLoadedImageTabOpenState()
                : new ImageTabOpenState());
    }

    public void SelectTextTab(ImageUserControl context) { }
}