using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Helpers
{
    public class ImageHelper
    {
        public static async Task<string> SaveImageAsync(IFormFile file, Guid userId, string wwwroot, string url)
        {
            if (file == null)
                return "";
            Directory.CreateDirectory(Path.Combine(wwwroot, "images"));

            var fileExtension = file.FileName.Split('.').Last();
            string fileName = Path.Combine(wwwroot, "images", userId.ToString() + "." + fileExtension);

            if (File.Exists(fileName))
                File.Delete(fileName);

            using var stream = System.IO.File.Create(fileName);
            await file.CopyToAsync(stream);

            return url + "/images/" + userId.ToString() + "." + fileExtension;
        }
    }
}
