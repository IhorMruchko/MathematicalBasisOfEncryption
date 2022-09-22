namespace EncryptionWPF.Controls;

public interface IFormatUserControl
{
    public void EncodeButtonClick();

    public void DecodeButtonClick();

    public void SaveFile();

    public void OpenFile();

    public void SetCypherControl(ICipherUserControl cypherControl);
}
