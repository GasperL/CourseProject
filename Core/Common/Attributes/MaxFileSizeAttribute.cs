using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.Common.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }

            return file.Length > _maxFileSize ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Максимальный размер фотографии {_maxFileSize} байтов.";
        }
    }
}