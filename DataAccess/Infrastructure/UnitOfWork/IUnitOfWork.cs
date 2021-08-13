using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Entities.Common.Repositories.GenericRepository;
using DataAccess.Entities.Common.Repositories.ProductRepository;
using DataAccess.Entities.Common.Repositories.UserRepository;

namespace DataAccess.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        
        IUserRepository Users { get; }
        
        IGenericRepository<Category> Categories { get; }
        
        IGenericRepository<Provider> Provider { get; }
        
        IGenericRepository<Manufacturer> Manufacturers { get; }
        
        IGenericRepository<ProductGroup> ProductGroups { get; }
        
        IGenericRepository<ProviderRequest> ProviderRequest { get; }

        Task Commit();
    }
}