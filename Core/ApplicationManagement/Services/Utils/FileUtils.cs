using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class FileUtils
    {
        public static string GetPhotoBase64(byte[] bytes)
        {
            return $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
        }

        public static Task<byte[]> GetFileBytes(IFormFile file)
        {
            using var binaryReader = new BinaryReader(file.OpenReadStream());
            var fileBytes = binaryReader.ReadBytes((int) file.Length);

            return Task.FromResult(fileBytes);
        }
    }
}