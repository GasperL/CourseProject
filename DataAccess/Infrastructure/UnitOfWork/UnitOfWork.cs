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

        public IGenericRepository<Category> Categories { get; }
        
        public IGenericRepository<Provider> Providers { get; }
        
        public IGenericRepository<Manufacturer> Manufacturers { get; }

        public IGenericRepository<ProductGroup> ProductGroups { get; }

        private readonly ApplicationContext _context;

        public UnitOfWork(
            ApplicationContext context,
            IProductRepository products,
            IUserRepository users,
            IGenericRepository<Category> category, 
            IGenericRepository<ProductGroup> productGroups, 
            IGenericRepository<Provider> providers, 
            IGenericRepository<Manufacturer> manufacturers)
        {
            _context = context;
            Products = products;
            Users = users;
            Categories = category;
            ProductGroups = productGroups;
            Providers = providers;
            Manufacturers = manufacturers;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}