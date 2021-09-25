#nullable enable
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.ApplicationManagement.Services.Utils;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartViewModel> GetCart(string userId)
        {
            var currentUserOrder = await _unitOfWork.UserOrder.GetSingle(
                isTracking: false, 
                filter: userOrder => userOrder.UserId == userId && userOrder.Status == OrderStatus.InCart,
                selector: userOrder => new
                {
                    userOrder.TotalPrice,
                    userOrder.User.BonusPoints,
                    OrderItemIds = userOrder.OrderItems.Select(oi => new
                    {
                        oi.Product.Price,
                        oi.Amount,
                        ProductId = oi.Product.Id,
                        oi.Product.ProductName,
                        DiscountPercentage = oi.Product.ProductGroup.Discount,
                        oi.Product.Photos,
                        ProductPrice = oi.Product.Price,
                        oi.UserOrderId,
                    }),
                });
    
            var orderItems = currentUserOrder.OrderItemIds.Select(x =>  new OrderItemViewModel
                {
                    DiscountPercentage = x.DiscountPercentage,
                    ProductId = x.ProductId,
                    UserOrderId = x.UserOrderId,
                    ProductName = x.ProductName,
                    Amount = x.Amount,
                    Price = x.Price
                }).ToArray();

            var initialPrice = currentUserOrder.OrderItemIds.Sum(oi => oi.Amount * oi.Price);
            var totalDiscount = orderItems.Sum(x =>
                ProductUtils.CalculateProductDiscountPercentages(x.Price, x.DiscountPercentage));
            var bonusPointsDiscount = ProductUtils.CalculateDiscountBonusPoints(currentUserOrder.BonusPoints);
            var totalPrice = currentUserOrder.TotalPrice - totalDiscount - bonusPointsDiscount;
            var bonusPoints = ProductUtils.CalculateBonusPoints(totalPrice);

            return new CartViewModel
            {
                OrderItems = orderItems,
                TotalPrice = totalPrice,
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

            await CreateNewOrderIfOrderNull(productId, userId, order, product);

            await AddNewOrderItemIfOrderNotNull(order, product);
            
            await _unitOfWork.Commit();
        }

        private async Task AddNewOrderItemIfOrderNotNull(UserOrder? order, Product product)
        {
            if (order != null)
            {
                order.OrderItems.Add(new OrderItem
                {
                    UserOrderId = order.Id,
                    ProductId = product.Id,
                    Amount = 1,
                });

                order.TotalPrice += product.Price;
                await _unitOfWork.UserOrder.Update(order);
            }
        }

        private async Task CreateNewOrderIfOrderNull(
            Guid productId, 
            string userId, 
            UserOrder? order, 
            Product product)
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
                    TotalPrice = product.Price
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