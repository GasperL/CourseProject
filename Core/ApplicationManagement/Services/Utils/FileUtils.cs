using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class FileUtils
    {
        public static string GetPhotoBase64(byte[] bytes)
        {
            return $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
        }

        public static byte[] GetFileBytes(IFormFile file)
        {
            using var binaryReader = new BinaryReader(file.OpenReadStream());
            
            return binaryReader.ReadBytes((int) file.Length);
        }
    }
}