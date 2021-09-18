using System.Collections.Generic;
using DataAccess.Entities;

namespace Core.Common.ViewModels.MainEntityViewModels
{
    public class UserOrderViewModel
    {
        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderItemViewModel> OrderItem { get; set; }
        
    }
}