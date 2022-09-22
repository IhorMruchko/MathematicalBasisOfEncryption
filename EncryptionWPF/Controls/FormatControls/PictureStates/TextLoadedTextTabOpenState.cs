namespace EncryptionWPF.Controls.FormatControls.PictureStates;

internal sealed class TextLoadedTextTabOpenState : TextTabOpenState
{ 
    public override void Decode(ImageUserControl imageControl)
        => DecodeImage(imageControl);

    public override void Encode(ImageUserControl imageControl)
        => EncodeImage(imageControl);

    public override void SaveFile(ImageUserControl imageControl)
        => SaveTextFile(imageControl);
}