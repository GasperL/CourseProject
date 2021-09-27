#nullable enable
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.Utils;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task Remove(Guid orderItemId)
        {
             await _unitOfWork.OrderItems.Delete(orderItemId);

             await _unitOfWork.Commit();
        }

        public async Task<CartViewModel?> GetCart(string userId)
        {
            var currentUserOrder = await _unitOfWork.UserOrder.GetSingle(
                isTracking: false,
                filter: userOrder => userOrder.UserId == userId && userOrder.Status == OrderStatus.InCart,
                selector: userOrder => new
                {
                    userOrder.User.BonusPoints,
                    OrderItemIds = userOrder.OrderItems.Select(oi => new
                    {
                        OrderItemId = oi.Id,
                        oi.Product.Price,
                        oi.Amount,
                        ProductId = oi.Product.Id,
                        DiscountPercentage = oi.Product.ProductGroup.Discount,
                        oi.Product.Photos,
                        ProductPrice = oi.Product.Price,
                        oi.UserOrderId,
                        oi.Product.CoverPhoto,
                        ProviderName = oi.Product.Provider.Name,
                        oi.Product.ProductName,
                        CategoryName = oi.Product.Category.Name,
                        ManufacturerName = oi.Product.Manufacturer.Name,
                        DiscountPrice = ProductUtils.CalculateProductDiscountPercentages(oi.Product.Price,
                            oi.Product.ProductGroup.Discount)
                    }),
                });

            var orderItems = currentUserOrder.OrderItemIds.Select(x => new OrderItemViewModel
            {
                Id = x.OrderItemId,
                DiscountPercentage = x.DiscountPercentage,
                ProductId = x.ProductId,
                UserOrderId = x.UserOrderId,
                ProductName = x.ProductName,
                ProviderName = x.ProviderName,
                ManufacturerName = x.ManufacturerName,
                CategoryName = x.CategoryName,
                Amount = x.Amount,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
                CoverPhotoBase64 = FileUtils.GetPhotoBase64(x.CoverPhoto.Image),
            }).ToArray();

            var initialPrice = currentUserOrder.OrderItemIds.Sum(oi =>oi.Price * oi.Amount);
            
            var totalDiscount =  orderItems
                .Where(x => x.DiscountPercentage > 0)
                .Sum(x => 
                (x.Price - ProductUtils.CalculateProductDiscountPercentages(x.Price, x.DiscountPercentage)) * x.Amount);
            
            var bonusPointsDiscount = ProductUtils.CalculateDiscountBonusPoints(currentUserOrder.BonusPoints);
            
            var price = initialPrice - totalDiscount - bonusPointsDiscount;
            
            var bonusPoints = ProductUtils.CalculateBonusPoints(initialPrice);

            return new CartViewModel
            {
                OrderItems = orderItems,
                TotalPrice = price,
                InitialPrice = initialPrice,
                DiscountAmount = totalDiscount,
                BonusPointsDiscount = bonusPointsDiscount,
                BonusPoints = bonusPoints
            };
        }

        public async Task Add(Guid productId, string userId)
        {
            var order = await GetOrder(userId);

            var product = await _unitOfWork.Products.GetEntityById(productId);

            await CreateNewOrderIfNull(productId, userId, order);

            await AddNewOrderItemIfNotNull(order, product);

            await _unitOfWork.Commit();
        }

        private async Task AddNewOrderItemIfNotNull(UserOrder? order, Product product)
        {
            if (order != null)
            {
                var orderItem = order.OrderItems.SingleOrDefault(x => x.ProductId == product.Id);

                if (orderItem == null)
                {
                    order!.OrderItems.Add(new OrderItem
                    {
                        UserOrderId = order.Id,
                        ProductId = product.Id,
                        Amount = 1,
                    });
                }
                else
                {
                    orderItem.Amount += 1;
                }

                await _unitOfWork.UserOrder.Update(order);
            }
        }
        
        private async Task CreateNewOrderIfNull(
            Guid productId,
            string userId,
            UserOrder? order)
        {
            if (order == null)
            {
                await _unitOfWork.UserOrder.Add(new UserOrder
                {
                    UserId = userId,
                    Id = Guid.NewGuid(),
                    OrderItems = new Collection<OrderItem>()
                    {
                        new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            Amount = 1,
                            ProductId = productId,
                            UserOrderId = Guid.NewGuid()
                        }
                    },
                });
            }
        }

        private async Task<UserOrder?> GetOrder(string userId)
        {
            var order = await _unitOfWork.UserOrder
                .GetSingleOrDefault(
                    isTracking: true,
                    filter: x => x.UserId == userId,
                    selector: s => s,
                    includeProperties: i => i.OrderItems);
            return order;
        }
    }
}