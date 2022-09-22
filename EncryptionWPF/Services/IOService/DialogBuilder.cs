using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace EncryptionWPF.Services.IOService;

public class DialogBuilder
{
    private const string INVALID_FORMAT_EXCEPTION = "Format [{0}] is not provided in the dialog";
    
    private const string INVALID_TITLE_LENGHT_EXCEPTION = "Lenght of the title ({0}) is out of bound";

    private const string DIRECTORY_NOT_EXISTS_ERROR = "Directory [{0}] is not exists";

    private readonly static Dictionary<DialogFormat, string> _filterSetter = new()
    {
        [DialogFormat.Text] = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
        [DialogFormat.Image] = "JPEG files|*.jpg|PNG files|*.png|Bitmaps|*.bmp|Image files|*.bmp;*.jpg;*.png;|All files|*.*"
    };

    private readonly OpenFileDialog _dialog = new();

    public DialogBuilder SetFormat(DialogFormat format)
    {
        if (_filterSetter.ContainsKey(format) == false)
        {
            throw new OperationCanceledException(
                string.Format(INVALID_FORMAT_EXCEPTION, format));
        }
        
        _dialog.Filter = _filterSetter[format];

        return this;
    }

    public DialogBuilder SetTitle(string title)
    {
        if (title.Length > 35)
        {
            throw new OperationCanceledException(
                string.Format(INVALID_TITLE_LENGHT_EXCEPTION, title.Length)); 
        }

        _dialog.Title = title;

        return this;
    }

    public DialogBuilder SetInitialDirectory(string directory)
    {
        if (Directory.Exists(directory) == false)
        {
            throw new OperationCanceledException(
                string.Format(DIRECTORY_NOT_EXISTS_ERROR, directory));
        }

        _dialog.InitialDirectory = directory;
        
        return this;
    }

    public FileDialog CreateSaveDialog()
    {
        return new SaveFileDialog
        {
            Filter = _dialog.Filter,
            Title = _dialog.Title,
            InitialDirectory = _dialog.InitialDirectory
        };
    }

    public FileDialog CreateOpenDialog()
    {
        return _dialog;
    }
}
