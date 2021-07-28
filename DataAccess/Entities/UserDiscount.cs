using System;

namespace DataAccess.Entities
{
    public class UserDiscount
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid PersonalDiscountId { get; set; }
        
        public PersonalDiscount PersonalDiscount { get; set; }
        
        public Guid BonusPointsId { get; set; }
        
        public BonusPoints BonusPoints { get; set; }
    }
}