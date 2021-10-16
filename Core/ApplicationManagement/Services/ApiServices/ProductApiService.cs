using System.Threading.Tasks;
using AutoMapper;
using Core.ApplicationManagement.Dtos;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ApiServices
{
    public class ProductApiService : IProductApiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductApiService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<ProductDto[]> GetAvailableProducts()
        {
            var product = await _unitOfWork.Products.GetList(
                isTracking: false,
                selector: prod => prod,
                filter: p => p.IsAvailable,
                p => p.ProductGroup,
                p => p.Photos,
                cf => cf.CoverPhoto);

            return _mapper.Map<ProductDto[]>(product);
        }
    }
}