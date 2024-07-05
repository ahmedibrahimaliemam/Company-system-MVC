using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Company.PL.Helper
{
    public class Documents
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
            string FilePath = Path.Combine(FolderPath, FileName);
            var fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(fs);
            return FileName;
        }
        public static void RemoveFile(string fileName, string FolderName)
        {
            if (fileName is not null && FolderName is not null)
            {
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, fileName);
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }
            }
        }
    }
}
