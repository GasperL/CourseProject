using System;

namespace Core.Common.ViewModels
{
    public class ProductGroupViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Discount { get; set; }

        public int BonusPoints { get; set; }
    }
}