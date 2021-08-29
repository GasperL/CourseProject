using System;

namespace DataAccess.Entities
{
    public class ProductPhoto : BaseEntity
    {
        public byte[] Image { get; set; }

        public Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}