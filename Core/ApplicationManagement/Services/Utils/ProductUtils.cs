﻿using System;
using DataAccess.Entities;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class ProductUtils
    {
        public static decimal CalculateProductDiscountPercentages(
            decimal productPrice, 
            double discountPercentage)
        {
            var discount = productPrice - ((productPrice * (decimal) discountPercentage) / 100);
            
            return productPrice == discount ? 0 : discount;
        }
        
        public static decimal CalculateDiscountBonusPoints(int bonusPoints)
        {
            if (bonusPoints > 50)
            {
                return  50 * (decimal)0.25;
            }
            
            return  bonusPoints * (decimal)0.25;
        }
        
        public static int CalculateBonusPoints(decimal totalPurchasePrice)
        {
            return (int) Math.Round(totalPurchasePrice / 10);
        }
    }
}