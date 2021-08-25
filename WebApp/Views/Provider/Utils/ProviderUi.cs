using Core.Common.ViewModels;
using DataAccess.Entities;

namespace WebApp.Views.Provider.Utils
{
    public static class ProviderUi
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
    }
}