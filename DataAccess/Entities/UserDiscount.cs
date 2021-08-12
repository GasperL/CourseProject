using System;

namespace DataAccess.Entities
{
    public class UserDiscount : BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }


        public int PersonalDiscount { get; set; }
        
        public int BonusPointsId { get; set; }
    }
}