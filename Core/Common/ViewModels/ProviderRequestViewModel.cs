using System;
using DataAccess.Entities;

namespace Core.Common.ViewModels
{
    public class ProviderRequestViewModel
    {
        public Guid Id { get; set; }
        
        public string UserId { get; set; }

        public Guid ProviderId { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public ProviderRequestStatusEnum Status { get; set; }
    }
}