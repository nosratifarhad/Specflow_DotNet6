using SpecflowDotNet6.Dtos;

namespace SpecflowDotNet6.Repositorys.ReadRepository;

public interface IProductReadRepository
{
    Task<ProductDto> GetProduct(int productId);

    Task<IEnumerable<ProductDto>> GetProducts();

    Task<bool> IsExistProduct(int productId);

}
