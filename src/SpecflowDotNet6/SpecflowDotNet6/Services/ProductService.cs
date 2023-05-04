﻿using SpecflowDotNet6.Dtos;
using SpecflowDotNet6.Entitys;
using SpecflowDotNet6.Repositorys.ReadRepository;
using SpecflowDotNet6.Repositorys.WriteRepository;
using SpecflowDotNet6.Services.Contract;
using SpecflowDotNet6.InputModels.ProductInputModels;
using SpecflowDotNet6.ViewModels.ProductViewModels;

namespace SpecflowDotNet6.Services;

public class ProductService : IProductService
{
    #region Fields

    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    #endregion Fields

    #region Ctor

    public ProductService(
        IProductReadRepository productReadRepository, 
        IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    #endregion Ctor

    #region Implement

    public async Task<ProductModel> GetProduct(int productId)
    {

        var productDto = await _productReadRepository.GetProduct(productId).ConfigureAwait(false);

        var productViewModel = CreateProductViewModelFromProductDto(productDto);

        return productViewModel;
    }

    public async Task<IEnumerable<ProductModel>> GetProducts()
    {
        var productDtos = await _productReadRepository.GetProducts().ConfigureAwait(false);
        if (productDtos == null || productDtos.Count() == 0)
            return Enumerable.Empty<ProductModel>();

        var productViewModels = CreateProductViewModelsFromProductDtos(productDtos);

        return productViewModels;
    }

    public async Task<int> CreateProductAsync(CreateProductInputModel inputModel)
    {
        ValidateProductName(inputModel.ProductName);

        ValidateProductTitle(inputModel.ProductTitle);

        var productEntoty = CreateProductEntityFromInputModel(inputModel);


        return await _productWriteRepository.CreateProductAsync(productEntoty).ConfigureAwait(false);
    }

    public async Task UpdateProductAsync(UpdateProductInputModel inputModel)
    {
        ValidateProductName(inputModel.ProductName);

        ValidateProductTitle(inputModel.ProductTitle);

        await IsExistProduct(int.Parse(inputModel.ProductId)).ConfigureAwait(false);

        var productEntoty = CreateProductEntityFromInputModel(inputModel);

        await _productWriteRepository.UpdateProductAsync(productEntoty).ConfigureAwait(false);
    }

    public async Task DeleteProductAsync(int productId)
    {
        await _productWriteRepository.DeleteProductAsync(productId).ConfigureAwait(false);
    }

    #endregion Implement

    #region Private

    private async Task IsExistProduct(int productId)
    {
        var isExistProduct = await _productReadRepository.IsExistProduct(productId).ConfigureAwait(false);
        if (isExistProduct == false)
            throw new Exception("productId Is Not Found.");
    }

    private Product CreateProductEntityFromInputModel(CreateProductInputModel inputModel)
        => new Product(inputModel.ProductName, inputModel.ProductTitle, inputModel.ProductDescription, inputModel.MainImageName, inputModel.MainImageTitle, inputModel.MainImageUri, inputModel.IsExisting, inputModel.IsFreeDelivery, inputModel.Weight);

    private Product CreateProductEntityFromInputModel(UpdateProductInputModel inputModel)
        => new Product(int.Parse(inputModel.ProductId), inputModel.ProductName, inputModel.ProductTitle, inputModel.ProductDescription, inputModel.MainImageName, inputModel.MainImageTitle, inputModel.MainImageUri, inputModel.IsExisting, inputModel.IsFreeDelivery, inputModel.Weight);

    private ProductModel CreateProductViewModelFromProductDto(ProductDto dto)
        => new ProductModel()
        {
            ProductId = dto.ProductId.ToString(),
            ProductName = dto.ProductName,
            ProductTitle = dto.ProductTitle,
            ProductDescription = dto.ProductDescription,
            MainImageName = dto.MainImageName,
            MainImageTitle = dto.MainImageTitle,
            MainImageUri = dto.MainImageUri,
            IsExisting = dto.IsExisting,
            IsFreeDelivery = dto.IsFreeDelivery,
            Weight = dto.Weight
        };

    private IEnumerable<ProductModel> CreateProductViewModelsFromProductDtos(IEnumerable<ProductDto> dtos)
    {
        ICollection<ProductModel> productViewModels = new List<ProductModel>();

        foreach (var ProductDto in dtos)
            productViewModels.Add(
                 new ProductModel()
                 {

                     ProductId = ProductDto.ProductId.ToString(),
                     ProductName = ProductDto.ProductName,
                     ProductTitle = ProductDto.ProductTitle,
                     ProductDescription = ProductDto.ProductDescription,
                     MainImageName = ProductDto.MainImageName,
                     MainImageTitle = ProductDto.MainImageTitle,
                     MainImageUri = ProductDto.MainImageUri,
                     IsExisting = ProductDto.IsExisting,
                     IsFreeDelivery = ProductDto.IsFreeDelivery,
                     Weight = ProductDto.Weight
                 });


        return productViewModels;
    }

    private void ValidateProductName(string productName)
    {
        if (string.IsNullOrEmpty(productName) || string.IsNullOrWhiteSpace(productName))
            throw new ArgumentNullException(nameof(productName), "Product Name must not be empty");
    }

    private void ValidateProductTitle(string productTitle)
    {
        if (string.IsNullOrEmpty(productTitle) || string.IsNullOrWhiteSpace(productTitle))
            throw new ArgumentNullException(nameof(productTitle), "Product Title must not be empty");
    }


    private ProductModel ToProductModel(UpdateProductInputModel inputModel)
        => new ProductModel()
        {
            ProductId = inputModel.ProductId,
            ProductName = inputModel.ProductName,
            ProductTitle = inputModel.ProductTitle,
            ProductDescription = inputModel.ProductDescription,
            MainImageName = inputModel.MainImageName,
            MainImageTitle = inputModel.MainImageTitle,
            MainImageUri = inputModel.MainImageUri,
            IsExisting = inputModel.IsExisting,
            IsFreeDelivery = inputModel.IsFreeDelivery,
            Weight = inputModel.Weight

        };

    private ProductModel ToProductModel(CreateProductInputModel inputModel)
        => new ProductModel()
        {
            ProductName = inputModel.ProductName,
            ProductTitle = inputModel.ProductTitle,
            ProductDescription = inputModel.ProductDescription,
            MainImageName = inputModel.MainImageName,
            MainImageTitle = inputModel.MainImageTitle,
            MainImageUri = inputModel.MainImageUri,
            IsExisting = inputModel.IsExisting,
            IsFreeDelivery = inputModel.IsFreeDelivery,
        };


    #endregion Private
}
