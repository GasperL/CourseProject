using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.ApplicationManagement.Services.ManufacturerService
{
    public interface IManufacturerService
    {
        Task Create(CreatingManufacturerOptions options);

        Task<ManufacturerViewModel[]> GetAll();
    }
}