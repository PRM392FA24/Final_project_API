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
    public class ShippingPriceController : ControllerBase
    {
        private readonly IShippingPriceService _service;

        public ShippingPriceController(IShippingPriceService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllShippingPrice()
        {
            try
            {
                var ShippingPrice = await _service.GetAllShippingPrice();
                return Ok(ShippingPrice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [AllowAnonymous]
        [Route("ID")]
        [HttpGet]
        public async Task<IActionResult> GetShippingPriceById(int ShippingPriceid)
        {
            var ShippingPrice = await _service.GetShippingPriceByID(ShippingPriceid);
            if (ShippingPrice == null)
            {
                return NotFound();
            }
            return Ok(ShippingPrice);
        }


        [Authorize(Roles = "2,3")]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddShippingPrice([FromBody] ShippingPrice ShippingPrice)
        {
            if (ShippingPrice == null)
            {
                return BadRequest("ShippingPrice object is null");
            }

            try
            {
                await _service.AddShippingPrice(ShippingPrice);
                return Ok("ShippingPrice added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "2,3")]
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateShippingPrice(int ShippingPriceid, [FromBody] ShippingPrice ShippingPrice)
        {
            if (ShippingPrice == null || ShippingPrice.ShippingId != ShippingPriceid)
            {
                return BadRequest("ShippingPrice data is invalid");
            }

            try
            {
                await _service.UpdateShippingPrice(ShippingPrice);
                return Ok("ShippingPrice updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "2,3")]
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteShippingPrice(int ShippingPriceid)
        {
            try
            {
                await _service.DeleteShippingPrice(ShippingPriceid);
                return Ok("ShippingPrice deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
