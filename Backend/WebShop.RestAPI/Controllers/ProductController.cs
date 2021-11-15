using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.IServices;
using WebShop.Core.Models;
using WebShop.RestAPI.DTOs.Products;

namespace WebShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public ActionResult<Product> Create([FromBody] ProductDto productDto)
        {
            var product = new Product
            {
                Desc = productDto.Desc,
                Img = productDto.Img,
                Name = productDto.Name
            };
            
            var productCreated = _productService.Create(product);

            var productReturned = new ProductDto
            {
                Name = productCreated.Name,
                Desc = productCreated.Desc,
                Img = productCreated.Img
            };
            
            return Ok(productReturned);
        }

        [HttpGet]
        public ActionResult<List<ProductDto>> Get()
        {
            return Ok(_productService.GetAll());
        }

        [HttpDelete]
        public ActionResult<Product> Delete(int id)
        {
            var productDeleted = _productService.Delete(_productService.Find(id));
            return Ok(productDeleted);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            return Ok(_productService.Find(id));
        }
    }
}
