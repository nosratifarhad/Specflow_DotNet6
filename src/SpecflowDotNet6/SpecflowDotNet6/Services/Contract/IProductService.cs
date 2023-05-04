
using SpecflowDotNet6.InputModels.ProductInputModels;
using SpecflowDotNet6.ViewModels.ProductViewModels;

namespace SpecflowDotNet6.Services.Contract;

public interface IProductService
{
    Task<int> CreateProductAsync(CreateProductInputModel inputModel);

    Task UpdateProductAsync(UpdateProductInputModel inputModel);

    Task DeleteProductAsync(int productId);

    Task<ProductModel> GetProduct(int productId);

    Task<IEnumerable<ProductModel>> GetProducts();

}
