using System.Threading.Tasks;
using AutoMapper;
using Core.ApplicationManagement.Dtos;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ApiServices
{
    public class CategoriesApiService : ICategoriesApiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesApiService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<CategoryDto[]> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetList(
                isTracking: false,
                selector: s => s);
            
            return _mapper.Map<CategoryDto[]>(categories);
        }
    }
}