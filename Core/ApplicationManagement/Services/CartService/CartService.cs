using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.ApplicationManagement.Services.Utils;
using Core.Common.ViewModels;
using Core.Common.ViewModels.MainEntityViewModels;
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
            var currentUserOrder = await _unitOfWork.UserOrder.GetSingleWithInclude(
                userOrder => userOrder.UserId == userId && userOrder.Status == OrderStatus.InCart, 
                userOrder => new
                {
                    userOrder.TotalPrice,
                    OrderItemIds = userOrder.OrderItems.Select(oi => new
                    {
                        oi.Price,
                        oi.Amount,
                        oi.Name,
                        ProductId = oi.Product.Id,
                        oi.Product.ProductName,
                        ProductDiscount = oi.Product.ProductGroup.Discount,
                        ProductPhotos = oi.Product.Photos,
                        ProductPrice = oi.Product.Price,
                        oi.UserOrderId,
                    }),
                });
            
            var orderItems = 
                currentUserOrder.OrderItemIds.Select(x =>  new OrderItemViewModel
                {
                    DiscountPercentage = x.ProductDiscount,
                    ProductId = x.ProductId,
                    UserOrderId = x.UserOrderId,
                    ProductName = x.ProductName,
                    Amount = x.Amount,
                    Price = x.Price
                }).ToArray();

            var userTotalOrderPrice = currentUserOrder.TotalPrice;

            var user = await _unitOfWork.Users.FindUserById(userId);

            var initialPrice = currentUserOrder.OrderItemIds.Sum(oi => oi.Amount * oi.Price);

            var totalDiscount = CalculateTotalDiscount(orderItems);

            var bonusPointsDiscount = ProductUtils.CalculateDiscountBonusPoints(user.BonusPoints);

            var totalPrice = userTotalOrderPrice - totalDiscount - bonusPointsDiscount;
                
            var bonusPoints = ProductUtils.EarnBonusPoints(totalPrice);

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
            var order = await _unitOfWork.UserOrder
                .GetSingleOrDefault(x => x.UserId == userId,
                    i => i.OrderItems);

            var product = await _unitOfWork.Products.GetEntityById(productId);

            if (order == null)
            {
                var userOrderId = Guid.NewGuid();

                await _unitOfWork.UserOrder.Add(new UserOrder
                {
                    UserId = userId,
                    Id = userOrderId,

                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            UserOrderId = userOrderId,
                            ProductId = product.Id,
                            Amount = 1,
                            Name = product.ProductName,
                            Price = product.Price
                        }
                    },
                    TotalPrice = product.Price
                });
            }
            else
            {
                order.OrderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    UserOrderId = order.Id,
                    ProductId = product.Id,
                    Amount = 1,
                    Name = product.ProductName,
                    Price = product.Price
                });

                order.TotalPrice += product.Price;
                
                await _unitOfWork.OrderItems.Update(order.OrderItems.Last());

                await _unitOfWork.UserOrder.Update(order);
            }

            await _unitOfWork.Commit();
        }

        private decimal CalculateTotalDiscount(OrderItemViewModel[] orderItems)
        {
            decimal discount = 0;

            foreach (var item in orderItems)
            {
                var discounts = ProductUtils.CalculateProductDiscountPercentages(
                    item.Price, item.DiscountPercentage);

                if (discounts == 0)
                {
                    discount += 0;
                }
                else
                {
                    discount += item.Price - ProductUtils.CalculateProductDiscountPercentages(
                        item.Price, item.DiscountPercentage);
                }
            }

            return discount;
        }
    }
}