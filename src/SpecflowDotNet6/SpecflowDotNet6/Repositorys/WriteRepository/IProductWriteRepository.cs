using SpecflowDotNet6.Entitys;

namespace SpecflowDotNet6.Repositorys.WriteRepository;

public interface IProductWriteRepository
{
    Task<int> CreateProductAsync(Product product);

    Task UpdateProductAsync(Product product);

    Task DeleteProductAsync(int productId);

}
