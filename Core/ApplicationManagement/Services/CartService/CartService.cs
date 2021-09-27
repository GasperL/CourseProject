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
            var orderId = await _unitOfWork.UserOrder.GetSingle(
                isTracking: false,
                filter: userOrder => userOrder.UserId == userId && userOrder.Status == OrderStatus.InCart,
                selector: order => order.Id);

            var viewModels = (await _unitOfWork.OrderItems.GetList(
                    isTracking: false,
                    filter: item => item.Id == orderId,
                    selector: oi => new OrderItemViewModel
                    {
                        Id = oi.Id,
                        Amount = oi.Amount,
                        Price = oi.Product.Price,
                        UserOrderId = oi.UserOrderId,
                        ProductId = oi.Product.Id,
                        DiscountPercentage = oi.Product.ProductGroup.Discount,
                        // сохранить на вьюшку byte[] данные кавер фото и после загрузки из бд конвертнуть в base64
                        CoverPhotoBase64 = FileUtils.GetPhotoBase64(oi.Product.CoverPhoto.Image),
                        ProviderName = oi.Product.Provider.Name,
                        ProductName = oi.Product.ProductName,
                        CategoryName = oi.Product.Category.Name,
                        ManufacturerName = oi.Product.Manufacturer.Name,
                        // посчитать после выгрузки данных
                        DiscountPrice = 0
                    })
                );

            var initialPrice = viewModels.Sum(oi => oi.Price * oi.Amount);
            
            var totalDiscount =  viewModels
                .Where(x => x.DiscountPercentage > 0)
                .Sum(x => 
                (x.Price - ProductUtils.CalculateProductDiscountPercentages(x.Price, x.DiscountPercentage)) * x.Amount);

            // IUserAccountService - добавь метод GetUserData, куда положи кол-во бонус поинтов и расширяй по необходимости
            var bonusPointsDiscount = ProductUtils.CalculateDiscountBonusPoints(currentUserOrder.BonusPoints);
            
            var price = initialPrice - totalDiscount - bonusPointsDiscount;
            
            var bonusPoints = ProductUtils.CalculateBonusPoints(initialPrice);

            return new CartViewModel
            {
                OrderItems = viewModels,
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
