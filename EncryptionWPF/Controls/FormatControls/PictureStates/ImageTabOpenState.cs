using EncryptionWPF.Services.IOService;

namespace EncryptionWPF.Controls.FormatControls.PictureStates;

public class ImageTabOpenState : ImageUserControlTools, IImageUserControlState
{
    public virtual void Encode(ImageUserControl context)
        => FileIsNotLoadedException();


    public virtual void Decode(ImageUserControl context)
        => FileIsNotLoadedException();

    public void OpenFile(ImageUserControl imageControl)
    {
        var dialog = new DialogBuilder()
            .SetTitle("Select image to open")
            .SetInitialDirectory(INITIAL_IMAGE_DIRECTORY)
            .SetFormat(DialogFormat.Image)
            .CreateOpenDialog();

        if (dialog.ShowDialog() == true)
        {
            var image = LoadBitmapImageFromUrl(dialog.FileName);
            imageControl.ImageContainer.Source = image;
            imageControl.TextImageContainer.Text = FromImageToBase64(image);
        }

        imageControl.SetState(new ImageLoadedImageTabOpenState());
    }

    public virtual void SaveFile(ImageUserControl imageControl)
        => FileIsNotLoadedException();

    public void SelectImageTab(ImageUserControl imageControl) { }

    public virtual void SelectTextTab(ImageUserControl imageControl)
    {
        var isAnyTextInTab = imageControl.TextImageContainer.Text != string.Empty;
        imageControl.SetState(
            isAnyTextInTab 
            ? new TextLoadedTextTabOpenState()
            : new TextTabOpenState());
    }
}
