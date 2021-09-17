using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.ApplicationManagement.Services.Utils;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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
            var userOrder = await _unitOfWork.UserOrders.GetWithIncludable(
                include => include
                    .Where(x => x.UserId == userId)
                    .Include(orderProduct => orderProduct.OrderItems)
                    .ThenInclude(product => product.Product)
                    .ThenInclude(productPhotos => productPhotos.Photos)
                    .Include(orderProductGroup => orderProductGroup.OrderItems)
                    .ThenInclude(product => product.Product)
                    .ThenInclude(group => group.ProductGroup));

            var orderItems = userOrder
                .Select(o => o.OrderItems.First())
                .ToArray();

            var userTotalOrderPrice = userOrder.Select(x => x.TotalPrice).First();

            var user = await _unitOfWork.Users.FindUserById(userId);

            var initialPrice = orderItems
                .Select(x => x.Product)
                .Sum(product => orderItems.Sum(x => x.Amount) * product.Price);

            var totalDiscount = CalculateTotalDiscount(orderItems);

            var bonusPointsDiscount = ProductUtils.CalculateBonusPoints(user.BonusPoints);

            var bonusPoints = userOrder
                .Select(x => x.OrderItems
                    .Sum(s => s.Amount))
                .Sum();

            var totalPrice = userTotalOrderPrice - totalDiscount - bonusPointsDiscount;

            var orderItemViewModel = _mapper.Map<OrderItemViewModel[]>(orderItems);

            return new CartViewModel
            {
                Product = orderItemViewModel,
                TotalPrice = totalPrice,
                InitialPrice = initialPrice,
                DiscountAmount = totalDiscount,
                BonusPointsDiscount = bonusPointsDiscount,
                BonusPoints = bonusPoints
            };
        }

        public async Task Add(Guid productId, string userId)
        {
            var order = await _unitOfWork.UserOrders
                .GetSingleOrDefault(x => x.UserId == userId,
                    i => i.OrderItems);

            var product = await _unitOfWork.Products.GetEntityById(productId);

            if (order == null)
            {
                var userOrderId = Guid.NewGuid();

                await _unitOfWork.UserOrders.Add(new UserOrder
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
                
                foreach (var item in order.OrderItems)
                {
                    await _unitOfWork.OrderItems.Update(item);
                }

                await _unitOfWork.UserOrders.Update(order);
            }

            await _unitOfWork.Commit();
        }

        private decimal CalculateTotalDiscount(OrderItem[] orderItems)
        {
            decimal discount = 0;

            foreach (var item in orderItems)
            {
                var discounts = ProductUtils.CalculateProductDiscountPercentages(item.Product);

                if (discounts == 0)
                {
                    discount += 0;
                }
                else
                {
                    discount += item.Price - ProductUtils.CalculateProductDiscountPercentages(item.Product);
                }
            }

            return discount;
        }
    }
}