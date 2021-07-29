using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductManagement
{
    public interface IProductGroupRepository
    {
        Task<Group> Add();

        Task Delete();

        Task GetAll();

        Task GetById();
    }
}