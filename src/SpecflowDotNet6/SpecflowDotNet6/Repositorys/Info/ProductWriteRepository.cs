using SpecflowDotNet6.Entitys;
using SpecflowDotNet6.Repositorys.WriteRepository;

namespace SpecflowDotNet6.Repositorys.Info;
public class ProductWriteRepository : IProductWriteRepository
{
    public async Task<int> CreateProductAsync(Product product)
    {
        await Task.Delay(100);
        return 1;
    }

    public async Task DeleteProductAsync(int productId)
    {
        await Task.Delay(100);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await Task.Delay(100);
    }
}