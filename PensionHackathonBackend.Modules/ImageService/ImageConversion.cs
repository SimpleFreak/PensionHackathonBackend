using System;
using System.IO;

namespace PensionHackathonBackend.Modules.ImageService;

public static class ImageConversion
{
    /* Получение картинки */
    public static string GetImage(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

        if (!File.Exists(filePath))
        {
            throw new Exception();
        }

        byte[] imageBytes = File.ReadAllBytes(filePath);
        string base64String = Convert.ToBase64String(imageBytes);

        return base64String;
    }
}