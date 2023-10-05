using System.IO.Compression;

namespace Web.ZipCompressor;

public static class ZipCompressor
{
    private const string ZipDirectoryPath = "../../../../../ZipFiles";
    
    private const string ZipFileName = "Web.zip";

    private static void CreateDirectoryForZipFile()
    {
        if (!Directory.Exists(ZipDirectoryPath))
        {
            Directory.CreateDirectory(ZipDirectoryPath);
        }
    }

    private static void RemoveDirectoryForZipFile()
    {
        if (Directory.Exists(ZipDirectoryPath))
        {
            Directory.Delete(ZipDirectoryPath);
        }
    }
    
    public static void Compress(string sourceFolder)
    {
        CreateDirectoryForZipFile();
        
        if (!File.Exists(AbsoluteZipFilePath)) 
        {
            ZipFile.CreateFromDirectory(sourceFolder, AbsoluteZipFilePath); 
        }
    }

    public static void RemoveZip()
    {
        if (File.Exists(AbsoluteZipFilePath))
        {
            File.Delete(AbsoluteZipFilePath);
        }
        
        RemoveDirectoryForZipFile();
    }

    public static string AbsoluteZipFilePath => ZipDirectoryPath + '/' + ZipFileName;
}