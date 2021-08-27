using Core.Common.ViewModels;
using DataAccess.Entities;

namespace WebApp.Views.Utils
{
    public static class UiHelper
    {
        public static string GetStatusColor(ProviderRequestViewModel item)
        {
            return item.Status switch
            {
                ProviderRequestStatus.Requested => "status-requested",
                ProviderRequestStatus.Approved => "status-approved",
                ProviderRequestStatus.Declined => "status-declined",
                _ => null
            };
        }
        
        public static string GetAvailabilitySmile(bool availability)
        {
            return availability == true ? "smile-smile fas fa-smile" : "smile-angry far fa-angry";
        }
    }
}