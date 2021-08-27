using System;

namespace Core.ApplicationManagement.Exceptions
{
    public class ProductActivateException : Exception
    {
        public int Amount { get; set; }

        public bool IsAvailable { get; set; }
        
        public Guid ProductId { get; set; }
        
        public ProductActivateException(string message) : base(message)
        {
            
        }
    }
}