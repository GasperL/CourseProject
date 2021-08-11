namespace DataAccess.Entities
{
    public class ProviderRequest : BaseEntity
    {
        public User User { get; set; }

        public string UserId { get; set; }
        
        public ProviderRequestStatusEnum Status { get; set; }
    }
}