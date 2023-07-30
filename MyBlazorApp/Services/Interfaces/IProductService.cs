using MyDtos;

namespace MyBlazorApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task AddProductAsync(CreateProductDto product);
        Task UpdateProductAsync(int id, ProductDto updateDto);
        Task DeleteProductAsync(int id);
    }
}
