using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using EncryptionWPF.Services.IOService;
using MathematicalBasisOfEncryption.CipherBase.Alphabets;

namespace EncryptionWPF.Controls.FormatControls.PictureStates;

public class ImageUserControlTools
{
    protected const string INITIAL_IMAGE_ENCODED_DIRECTORY =
        @"E:\Programming\C#\MathematicalBasisOfEncryption\EncryptionWPF\Resourse\Image\Encoded Images\";

    protected const string INITIAL_IMAGE_DIRECTORY =
        @"E:\Programming\C#\MathematicalBasisOfEncryption\EncryptionWPF\Resourse\Image\";

    protected static void FileIsNotLoadedException()
    {
        MessageBox.Show(
            "There is no file loaded",
            "Load file first",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }

    protected static void ImageCannotConvertException()
    {
        MessageBox.Show(
           "Image can not be converted",
           "Bad encoding",
           MessageBoxButton.OK,
           MessageBoxImage.Error);
    }

    protected static void SaveTextFile(ImageUserControl imageControl)
    {
        var dialog = new DialogBuilder()
            .SetTitle("Save image as base 64 file")
            .SetFormat(DialogFormat.Text)
            .SetInitialDirectory(INITIAL_IMAGE_ENCODED_DIRECTORY)
            .CreateSaveDialog();

        if (dialog.ShowDialog() == true)
        {
            File.WriteAllText(dialog.FileName, imageControl.TextImageContainer.Text);
        }
    }

    protected static void SaveImageFile(ImageUserControl context)
    {
        var dialog = new DialogBuilder()
            .SetTitle("Save image")
            .SetFormat(DialogFormat.Image)
            .SetInitialDirectory(INITIAL_IMAGE_DIRECTORY)
            .CreateSaveDialog();

        if (dialog.ShowDialog() == true)
        {
            SaveImage(context, dialog.FileName);
        }
    }

    protected static void EncodeImage(ImageUserControl imageControl)
    {
        var imageInBase64 = imageControl.TextImageContainer.Text.Trim();

        var imageEncoder = imageControl.CurrentCypherUserControl.Cipher.SetAlphabet(new ImageCodingAlphabet());

        imageControl.TextImageContainer.Text = imageEncoder.Encode(imageInBase64);
    }

    protected static void DecodeImage(ImageUserControl imageControl)
    {
        var imageInBase64 = imageControl.TextImageContainer.Text.Trim();

        var imageEncoder = imageControl.CurrentCypherUserControl.Cipher.SetAlphabet(new ImageCodingAlphabet());

        imageControl.TextImageContainer.Text = imageEncoder.Decode(imageInBase64);
    }

    protected static string FromImageToBase64(BitmapImage image)
    {
        if (image == null)
        {
            return string.Empty;
        }

        var imageBytesRepresentation = ConvertImageToBytes(image);

        return Convert.ToBase64String(imageBytesRepresentation);
    }

    protected static bool ToImageFromBase64(ImageUserControl imageContext, out BitmapImage bitmapImage)
    {
        bitmapImage = new BitmapImage();

        try
        {
            bitmapImage = LoadBitmapImageFromBase64(imageContext.TextImageContainer.Text);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected static BitmapImage LoadBitmapImageFromUrl(string filePath)
    {
        var bitmapImage = new BitmapImage();

        bitmapImage.BeginInit();
        bitmapImage.UriSource = new Uri(filePath);
        bitmapImage.EndInit();

        return bitmapImage;
    }

    private static BitmapImage LoadBitmapImageFromBase64(string text)
    {
        var bitmapImage = new BitmapImage();
        var imageBytes = Convert.FromBase64String(text);

        bitmapImage.BeginInit();
        bitmapImage.StreamSource = new MemoryStream(imageBytes);
        bitmapImage.EndInit();

        return bitmapImage;
    }

    private static byte[] ConvertImageToBytes(BitmapImage image)
    {
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(image));

        using var stream = new MemoryStream();

        encoder.Save(stream);
        return stream.ToArray();
    }

    private static void SaveImage(ImageUserControl imageControl, string path)
    {
        var image = (BitmapFrame)imageControl.ImageContainer.Source;

        using var fs = new FileStream(path, FileMode.Create);

        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(image));
        encoder.Save(fs);
    }
}