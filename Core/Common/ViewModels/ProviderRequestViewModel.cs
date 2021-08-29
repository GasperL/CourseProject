using System;
using DataAccess.Entities;

namespace Core.Common.ViewModels
{
    public class ProviderRequestViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public ProviderRequestStatus Status { get; set; }
    }
}