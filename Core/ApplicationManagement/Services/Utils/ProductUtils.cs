using DataAccess.Entities;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class ProductUtils
    {
        public static decimal CalculateProductDiscountPercentages(Product product)
        {
            return product.Price - ((product.Price * (decimal) product.ProductGroup.Discount) / 100);
        }
    }
}