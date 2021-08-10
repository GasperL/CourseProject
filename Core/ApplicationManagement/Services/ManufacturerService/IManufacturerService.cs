using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.ApplicationManagement.Services.ManufacturerService
{
    public interface IManufacturerService
    {
        Task Create(CreateManufacturerViewModel viewModel);

        Task<ManufacturerViewModel[]> GetAll();
    }
}