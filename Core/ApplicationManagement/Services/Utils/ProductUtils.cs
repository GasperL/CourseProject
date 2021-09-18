using System;
using DataAccess.Entities;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class ProductUtils
    {
        public static decimal CalculateProductDiscountPercentages(decimal productPrice, double discountPercentage)
        {
            var discount = productPrice - ((productPrice * (decimal) discountPercentage) / 100);

            var ret = productPrice == discount ? 0 : discount;
            
            return ret;
        }
        
        public static decimal CalculateDiscountBonusPoints(int bonusPoints)
        {
            if (bonusPoints > 50)
            {
                return  50 * (decimal)0.25;
            }
            
            return  bonusPoints * (decimal)0.25;
        }
        
        public static int EarnBonusPoints(decimal totalPurchasePrice)
        {
            return (int) Math.Round(totalPurchasePrice / 10);
        }
    }
}