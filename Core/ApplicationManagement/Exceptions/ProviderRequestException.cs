using System;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.ApplicationManagement.Exceptions
{
    public class ProviderRequestException :  Exception
    {
        public ProviderRequestStatus Status { get; set; }
        
        public ProviderRequestException(string message, ProviderRequestStatus status) : base(message)
        {
            Status = status;
        }
    }
}