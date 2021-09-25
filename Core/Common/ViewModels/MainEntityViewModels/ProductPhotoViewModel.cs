using System;

namespace Core.Common.ViewModels.MainEntityViewModels
{
    public class ProductPhotoViewModel
    {
        public Guid ProductId { get; set; }
        
        public Guid Id { get; set; }

        public byte[] Image { get; set; }
    }
}