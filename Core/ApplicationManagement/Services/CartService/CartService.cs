using System;
using System.Collections.Generic;
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
            var currentUserOrder = await _unitOfWork.UserOrder.GetSingleWithInclude(
                userOrder => userOrder.UserId == userId && userOrder.Status == OrderStatus.InCart, 
                userOrder => new
                {
                    userOrder.TotalPrice,
                    userOrder.User.BonusPoints,
                    OrderItemIds = userOrder.OrderItems.Select(oi => new
                    {
                        oi.Product.Price,
                        oi.Amount,
                        ProductId = oi.Product.Id,
                        oi.Product.ProductName,
                        ProductDiscount = oi.Product.ProductGroup.Discount,
                        oi.Product.Photos,
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

            var initialPrice = currentUserOrder.OrderItemIds.Sum(oi => oi.Amount * oi.Price);

            var totalDiscount = CalculateTotalDiscount(orderItems);

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
                    OrderItems = new Collection<OrderItem>()
                    {
                        new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            Amount = 1,
                            ProductId = productId,
                            UserOrderId = userOrderId
                        }
                    },
                    TotalPrice = product.Price
                });
            }

            if (order != null)
            {
                order.OrderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    UserOrderId = order.Id,
                    ProductId = product.Id,
                    Amount = 1,
                });

                order.TotalPrice += product.Price;
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