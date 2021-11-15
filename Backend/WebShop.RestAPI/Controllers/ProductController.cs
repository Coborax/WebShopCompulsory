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

        [HttpPut("{id}")]
        public ActionResult<ProductDto> UpdateProduct(int id, PutProductDto updatdedProduct)
        {
            if (id < 1 || id != updatdedProduct.Id)
            {
                return BadRequest("Product id and id must match.");
            }
            Product updatedProduct = new Product
                {Desc = updatdedProduct.Desc, Name = updatdedProduct.Name, Id = updatdedProduct.Id};
                var updatedNewProduct = _productService.UpdateProduct(updatedProduct);
            return Ok(updatedNewProduct);
        }
    }
}
