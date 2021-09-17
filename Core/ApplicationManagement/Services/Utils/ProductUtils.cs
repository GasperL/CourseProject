using DataAccess.Entities;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class ProductUtils
    {
        public static decimal CalculateProductDiscountPercentages(Product product)
        {
            var discount = product.Price - ((product.Price * (decimal) product.ProductGroup.Discount) / 100);

            var ret = product.Price == discount ? 0 : discount;
            
            return ret;
        }
        
        public static decimal CalculateBonusPoints(int bonusPoints)
        {
            if (bonusPoints > 50)
            {
                return  50 * (decimal)0.25;
            }
            
            return  bonusPoints * (decimal)0.25;
        }
    }
}