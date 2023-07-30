using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDataAccess.Entities;
using MyDtos;
using MyRepository.Implementations;
using MyRepository.Interfaces;


namespace MyApiApp.Controllers
{
    [Authorize]
    [Route("api/Product")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        // GET: api/Product        
        [HttpGet]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            try
            {
                var _productList = await _product.GetAllProductsAsync();
                if (_productList == null || !_productList.Any())
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<IEnumerable<ProductDto>>(_productList));
            }
            catch (Exception ex)
            {
                return HandleException(ex, "An error occurred while getting the product list. Please try again later.");
            }
        }

        // GET api/Product/5
        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var productItem = await _product.GetProductByIdAsync(id);
                if (productItem == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ProductDto>(productItem));
            }
            catch (Exception ex)
            {
                return HandleException(ex, "An error occurred while getting the product detail. Please try again later.");
            }
        }

        // POST api/Product
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateProductDto createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Product model = _mapper.Map<Product>(createDTO);

                await _product.AddProductAsync(model);

                return CreatedAtRoute("GetProduct", new { id = model.Id }, createDTO);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "An error occurred while adding the product. Please try again later.");
            }
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductDto productDto)
        {
            try
            {
                // Check if the IDs in the URL and the DTO match
                if (id != productDto.Id)
                {
                    return BadRequest("The ID in the URL path does not match the ID in the request body.");
                }

                var existingProduct = await _product.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                var update = _mapper.Map(productDto, existingProduct);

                await _product.UpdateProductAsync(existingProduct);

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex, "An error occurred while updating the product. Please try again later.");
            }
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var productItem = await _product.GetProductByIdAsync(id);
                if (productItem == null)
                {
                    return NotFound();
                }

                await _product.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex, "An error occurred while deleting the product. Please try again later.");
            }
        }
    }

}