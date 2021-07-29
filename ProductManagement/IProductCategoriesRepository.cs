using System.Threading.Tasks;
using DataAccess.Entities;

namespace ProductManagement
{
    public interface IProductCategoriesRepository
    {
        Task<Category> Add();

        Task Delete();

        Task GetAll();

        Task GetById();
    }
}