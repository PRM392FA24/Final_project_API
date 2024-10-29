using BusinessObj.Models;
using DataAccessObj.DTO;
using DataAccessObj.DTO.ProductDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Services;

namespace GracefullFloristAPI.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductControl : ControllerBase
    {
        private readonly IProductService _service;

        public ProductControl(IProductService service)
        {
            _service = service;
        }

        [Route("{User}")]
        [HttpGet]
        public async Task<IActionResult> GetCustomize([FromRoute] string User)
        {
            ResponseType<List<Product>> response = new ResponseType<List<Product>>();
            try
            {
                response.Data = await this._service.GetCustomize(User);
                response.message = "success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(response);
            }
        }


        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            ResponseType<List<Product>> response = new ResponseType<List<Product>>();
            try
            {
                response.Data = await _service.GetAllProduct();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(response);
            }
        }

        [Route("{ProductID}")]
        [HttpGet]
        public async Task<IActionResult> GetProductById([FromRoute] string ProductID)
        {
            ResponseType<Product> response = new ResponseType<Product>();
            try
            {
                response.Data = await this._service.GetProductByID(ProductID);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                response.message=ex.Message;
                return BadRequest(response);
            }
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDTO Product)
        {
            ResponseType<string> response = new ResponseType<string>();
            try
            {
                response.Data = await this._service.AddProduct(Product);
                return Ok(response);
            }catch(Exception ex)
            {
                response.message=ex.Message;
                return BadRequest(response);
            }
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDTO Product)
        {
            ResponseType<string> response = new ResponseType<string>();
            try
            {
                response.Data = await this._service.UpdateProduct(Product);
                return Ok(response);
            }catch(Exception ex)
            {
                response.message=ex.Message;
                return BadRequest(response);
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(DeleteProductDTO request)
        {
            ResponseType<string> response = new ResponseType<string>();
            try
            {
                response.Data = await this._service.DeleteProduct(request);
                return Ok(response);
            }catch(Exception ex)
            {
                response.message=ex.Message;
                return BadRequest(response);
            }
        }
    }
}
