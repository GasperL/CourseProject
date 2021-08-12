using System;

namespace DataAccess.Entities
{
    public class ApprovedProvider : BaseEntity
    {
        public Provider Provider { get; set; }

        public Guid ProviderId { get; set; }
    }
}