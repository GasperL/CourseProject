using System;

namespace DataAccess.Entities
{
    public class UserDiscount : BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public Guid PersonalDiscountId { get; set; }
        
        public PersonalDiscount PersonalDiscount { get; set; }
        
        public Guid BonusPointsId { get; set; }
        
        public BonusPoints BonusPoints { get; set; }
    }
}