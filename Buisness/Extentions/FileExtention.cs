using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Buisness.Extentions
{
    public static class FileExtention
    {
        public static bool CheckFileContentType(this IFormFile file, string contentType)
        {
            return file.ContentType == contentType;
        }
        public static bool CheckFileLength(this IFormFile file, short lenth)
        {
            return (file.Length / 1024) <= lenth;
        }
        public async static Task<string> CreateFileAsync(this IFormFile file,IWebHostEnvironment _webHostEnvironment, params string[] folders)
        {
            string fileName = $"{Guid.NewGuid()}-{file.FileName}";

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath);

            foreach (string folder in folders)
            {
                filePath = Path.Combine(filePath, folder);
            }

            filePath = Path.Combine(filePath, fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        public static void DeleteFile(string fileName, IWebHostEnvironment env, params string[] folders)
        {
            string filePath = Path.Combine(env.WebRootPath);

            foreach (string folder in folders)
            {
                filePath = Path.Combine(filePath, folder);
            }

            filePath = Path.Combine(filePath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
