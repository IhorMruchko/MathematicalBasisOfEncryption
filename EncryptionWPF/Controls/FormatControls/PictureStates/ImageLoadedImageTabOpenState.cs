namespace EncryptionWPF.Controls.FormatControls.PictureStates;

public sealed class ImageLoadedImageTabOpenState : ImageTabOpenState
{
    public override void Decode(ImageUserControl context) 
        => DecodeImage(context);

    public override void Encode(ImageUserControl context) 
        => EncodeImage(context);

    public override void SaveFile(ImageUserControl context) 
        => SaveImageFile(context);
}