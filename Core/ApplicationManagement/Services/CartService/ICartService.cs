#nullable enable
using System;
using System.Threading.Tasks;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.CartService
{
    public interface ICartService
    {
        Task Add(Guid productId, string userId);

        Task<CartViewModel?> GetCart(string userId);
        
        Task Remove(Guid orderItemId);
    }
}