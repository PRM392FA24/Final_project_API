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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1")]
        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllFeedback()
        {
            try
            {
                var Feedback = await _service.GetAllFeedback();
                return Ok(Feedback);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "1")]
        [Route("ID")]
        [HttpGet]
        public async Task<IActionResult> GetFeedbackById(string id)
        {
            var Feedback = await _service.GetFeedbackByID(id);
            if (Feedback == null)
            {
                return NotFound();
            }
            return Ok(Feedback);
        }



        [Authorize(Roles = "1")]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddFeedback([FromBody] Feedback Feedback)
        {
            if (Feedback == null)
            {
                return BadRequest("Feedback object is null");
            }

            try
            {
                await _service.AddFeedback(Feedback);
                return Ok("Feedback added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "1")]
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateFeedback(string id, [FromBody] Feedback Feedback)
        {
            if (Feedback == null || Feedback.FeedbackId != id)
            {
                return BadRequest("Feedback data is invalid");
            }

            try
            {
                await _service.UpdateFeedback(Feedback);
                return Ok("Feedback updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "1")]
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFeedback(string id)
        {
            try
            {
                await _service.DeleteFeedback(id);
                return Ok("Feedback deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
