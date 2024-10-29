using BusinessObj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GracefullFloristAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _service;

        public OrderDetailController(IOrderDetailService service)
        {
            _service = service;
        }

        [Authorize(Roles = "2,3")]
        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetail()
        {
            try
            {
                var OrderDetail = await _service.GetAllOrderDetail();
                return Ok(OrderDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "1")]
        [Route("ID")]
        [HttpGet]
        public async Task<IActionResult> GetOrderDetailById(string orderid, string productid)
        {
            var OrderDetail = await _service.GetOrderDetailByID(orderid, productid);
            if (OrderDetail == null)
            {
                return NotFound();
            }
            return Ok(OrderDetail);
        }


        [Authorize(Roles = "1")]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddOrderDetail([FromBody] OrderDetail OrderDetail)
        {
            if (OrderDetail == null)
            {
                return BadRequest("OrderDetail object is null");
            }

            try
            {
                await _service.AddOrderDetail(OrderDetail);
                return Ok("OrderDetail added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "1")]
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(string orderid, string productid, [FromBody] OrderDetail OrderDetail)
        {
            if (OrderDetail == null || OrderDetail.OrderId != orderid || OrderDetail.ProductId != productid)
            {
                return BadRequest("OrderDetail data is invalid");
            }

            try
            {
                await _service.UpdateOrderDetail(OrderDetail);
                return Ok("OrderDetail updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "1")]
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderDetail(string orderid, string productid)
        {
            try
            {
                await _service.DeleteOrderDetail(orderid, productid);
                return Ok("OrderDetail deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
