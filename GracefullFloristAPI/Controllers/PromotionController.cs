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
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _service;

        public PromotionController(IPromotionService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1")]
        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllPromotion()
        {
            try
            {
                var Promotion = await _service.GetAllPromotion();
                return Ok(Promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "1")]
        [Route("ID")]
        [HttpGet]
        public async Task<IActionResult> GetPromotionById(string Promotionid)
        {
            var Promotion = await _service.GetPromotionByID(Promotionid);
            if (Promotion == null)
            {
                return NotFound();
            }
            return Ok(Promotion);
        }


        [Authorize(Roles = "2,3")]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddPromotion([FromBody] Promotion Promotion)
        {
            if (Promotion == null)
            {
                return BadRequest("Promotion object is null");
            }

            try
            {
                await _service.AddPromotion(Promotion);
                return Ok("Promotion added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "2,3")]
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdatePromotion(string Promotionid, [FromBody] Promotion Promotion)
        {
            if (Promotion == null || Promotion.PromotionId != Promotionid)
            {
                return BadRequest("Promotion data is invalid");
            }

            try
            {
                await _service.UpdatePromotion(Promotion);
                return Ok("Promotion updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "2,3")]
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeletePromotion(string Promotionid)
        {
            try
            {
                await _service.DeletePromotion(Promotionid);
                return Ok("Promotion deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
