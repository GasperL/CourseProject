﻿using System;
using System.Collections.Generic;
using System.IO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class FileUtils
    {
        public static ICollection<string> GetPhotoBase64(IEnumerable<byte[]> photos)
        {
            var base64Photo = new List<string>();

            foreach (var photo in photos)
            {
                base64Photo.Add($"data:image/jpeg;base64,{Convert.ToBase64String(photo)}");
            }

            return base64Photo;
        }
        
        public static string GetPhotoBase64(byte[] photo)
        {
            return $"data:image/jpeg;base64,{Convert.ToBase64String(photo)}";
        }

        public static byte[] GetFileBytes(IFormFile file)
        {
            using var binaryReader = new BinaryReader(file.OpenReadStream());
            
            return binaryReader.ReadBytes((int) file.Length);
        }
    }
}