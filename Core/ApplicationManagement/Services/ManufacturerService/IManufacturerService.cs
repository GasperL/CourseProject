using System;
using System.Threading.Tasks;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ManufacturerService
{
    public interface IManufacturerService
    {
        Task Create(ManufacturerViewModel viewModel);

        Task<ManufacturerViewModel[]> GetAll();
        
        Task Remove(Guid categoryId);
       
        Task Edit(ManufacturerViewModel model);
        
        Task<ManufacturerViewModel> GetManufacturerViewModel(Guid manufacturerId);
    }
}