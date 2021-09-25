using System.Collections.Generic;
using DataAccess.Entities;

namespace Core.Common.ViewModels
{
    public class CartViewModel
    {
        public ICollection<OrderItemViewModel> OrderItems { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal InitialPrice { get; set; }
        
        public decimal DiscountAmount { get; set; }
        
        public decimal BonusPointsDiscount { get; set; }
        
        public int BonusPoints { get; set; }
    }
}