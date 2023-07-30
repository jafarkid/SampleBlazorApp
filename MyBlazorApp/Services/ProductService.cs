using Blazored.LocalStorage;
using MyBlazorApp.Services.Interfaces;
using MyDtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MyBlazorApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;


        public ProductService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var products = await this._httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
            return products;
        }

        public Task<ProductDto> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddProductAsync(CreateProductDto createDto)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await this._httpClient.PostAsJsonAsync<CreateProductDto>("api/Product", createDto);
            if (response.IsSuccessStatusCode)
            {
                // Product successfully created
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task UpdateProductAsync(int id, ProductDto updateDto)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJsonAsync($"api/Product/{id}", updateDto);
            if (response.IsSuccessStatusCode)
            {
                // Product successfully updated
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                // Product successfully deleted
            }
            else
            {
                // Handle error, e.g., throw an exception, show error message, etc.
                throw new NotImplementedException();
            }
        }
    }
}
