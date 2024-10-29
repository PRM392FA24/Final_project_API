using BusinessObj.Models;
using DataAccessObj.DTO.OrderDTO;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1")]
        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var Order = await _service.GetAllOrder();
                return Ok(Order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "1")]
        [Route("ID")]
        [HttpGet]
        public async Task<IActionResult> GetOrderById(string orderid)
        {
            var Order = await _service.GetOrderByID(orderid);
            if (Order == null)
            {
                return NotFound();
            }
            return Ok(Order);
        }


        [Authorize(Roles = "1")]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order Order)
        {
            if (Order == null)
            {
                return BadRequest("Order object is null");
            }

            try
            {
                await _service.AddOrder(Order);
                return Ok("Order added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "1")]
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(string orderid, [FromBody] Order Order)
        {
            if (Order == null || Order.OrderId != orderid )
            {
                return BadRequest("Order data is invalid");
            }

            try
            {
                await _service.UpdateOrder(Order);
                return Ok("Order updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "1")]
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(DeleteOrderDTO request)
        {
            try
            {
                await _service.DeleteOrder(request);
                return Ok("Order deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
