using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductManagement
{
    public interface IProductGroupRepository
    {
        Task Add(Group group);

        Task Delete(Group id);

        Task GetAll();

        Task GetById(Guid id);
    }
}