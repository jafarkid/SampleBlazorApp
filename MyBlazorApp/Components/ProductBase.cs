using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using MyBlazorApp.Services.Interfaces;
using MyDtos;

namespace MyBlazorApp.Components
{
    public class ProductBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
        protected IEnumerable<ProductDto> Products { get; private set; }
        private IEnumerable<ProductDto> _Products { get; set; }

        protected int currentPage = 1;

        private const int itemsPerPage = 4;
        public int TotalPages => (int)Math.Ceiling((double)_Products.Count() / itemsPerPage);

        protected string newProductName;
        protected string newProductDescription;

        protected string productNameError = string.Empty;
        protected string productDescriptionError = string.Empty;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    _Products = await ProductService.GetProductsAsync();
                    UpdateDisplayedProducts();
                    StateHasChanged();
                    ToastService.ShowSuccess("Product loaded successfully.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Redirect to the login page
                //navigationManager.NavigateTo("login");
            }
        }

        protected async Task LoadProducts(bool lastPage = false)
        {
            _Products = await ProductService.GetProductsAsync();
            if (lastPage)
            {
                currentPage = TotalPages;
            }
            UpdateDisplayedProducts();
        }

        protected void ChangePage(int page)
        {
            currentPage = page;
            UpdateDisplayedProducts();
        }

        private void UpdateDisplayedProducts(bool reload = false)
        {
            var startIndex = (currentPage - 1) * itemsPerPage;
            Products = _Products.Skip(startIndex).Take(itemsPerPage).ToList();
        }

        protected async Task DeleteProduct(int id)
        {
            await ProductService.DeleteProductAsync(id);
            await LoadProducts();
            ToastService.ShowSuccess("Product deleted successfully.");
        }

        protected async Task AddProduct(string name, string description)
        {
            productNameError = string.IsNullOrEmpty(name) ? "Product name is required." : string.Empty;
            productDescriptionError = string.IsNullOrEmpty(description) ? "Product description is required." : string.Empty;

            if (string.IsNullOrEmpty(productNameError) && string.IsNullOrEmpty(productDescriptionError))
            {
                var createDto = new CreateProductDto { Name = name, Description = description };
                await ProductService.AddProductAsync(createDto);
                await LoadProducts(true);

                ToastService.ShowSuccess("Product added successfully.");

                // Clear the text in the input fields
                newProductName = string.Empty;
                newProductDescription = string.Empty;
            }
        }

        protected async Task UpdateProductAsync(int id, ProductDto updateDto)
        {
            await ProductService.UpdateProductAsync(id, updateDto);
            await LoadProducts();
            ToastService.ShowSuccess("Product updated successfully.");
        }
    }
}
