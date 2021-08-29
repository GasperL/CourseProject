using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Core.Common.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }
    
        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }
            
            var extension = Path.GetExtension(file.FileName);
            var isContains = _extensions.Contains(extension?.ToLower());
                
            return !isContains ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Данный формат картинки не поддерживается";
        }
    }
}