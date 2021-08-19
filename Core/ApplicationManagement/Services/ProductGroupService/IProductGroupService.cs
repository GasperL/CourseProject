using System;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ProductGroupService
{
    public interface IProductGroupService 
    {
        Task Create(CreateProductGroupViewModel options);

        Task<ProductGroupViewModel[]> GetAll();

        Task Remove(Guid id);
    }
}