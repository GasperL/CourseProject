using Core.Common.ViewModels;
using DataAccess.Entities;

namespace WebApp.Views.Provider.Utils
{
    public class ProviderUi
    {
        public static string StatusColor(ProviderRequestViewModel item)
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