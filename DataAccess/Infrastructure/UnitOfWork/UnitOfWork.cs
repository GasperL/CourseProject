

using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Entities.Common.Repositories.GenericRepository;
using DataAccess.Entities.Common.Repositories.ProductRepository;
using DataAccess.Entities.Common.Repositories.UserRepository;

namespace DataAccess.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; }
       
        public IUserRepository Users { get; }

        public IGenericRepository<ProductPhoto> Files { get;}

        public IGenericRepository<Category> Categories { get; }
        
        public IGenericRepository<Provider> Provider { get; }
        
        public IGenericRepository<ProviderRequest> ProviderRequest { get; }

        public IGenericRepository<Manufacturer> Manufacturers { get; }

        public IGenericRepository<ProductGroup> ProductGroups { get; }
        
        private readonly ApplicationContext _context;

        public UnitOfWork(
            ApplicationContext context,
            IProductRepository products,
            IUserRepository users,
            IGenericRepository<Category> category, 
            IGenericRepository<ProductGroup> productGroups, 
            IGenericRepository<Manufacturer> manufacturers, 
            IGenericRepository<ProviderRequest> providerRequest, 
            IGenericRepository<Provider> provider, 
            IGenericRepository<ProductPhoto> files)
        {
            _context = context;
            Products = products;
            Users = users;
            Categories = category;
            ProductGroups = productGroups;
            Manufacturers = manufacturers;
            ProviderRequest = providerRequest;
            Provider = provider;
            Files = files;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}