using BusinessObj.Models;
using DataAccessObj.DTO;
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
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeService _service;

        public AttributesController(IAttributeService service)
        {
            _service = service;
        }

        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllAttributes()
        {
            ResponseType<List<BusinessObj.Models.Attribute>> response = new ResponseType<List<BusinessObj.Models.Attribute>>();
            try
            {
                response.Data = await this._service.GetAllAttribute();
                return Ok(response); 
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(response);
            }
        }

        [Route("ID")]
        [HttpGet]
        public async Task<IActionResult> GetAttributeById(string id)
        {
            var attribute = await _service.GetAttributeByID(id);
            if (attribute == null)
            {
                return NotFound(); 
            }
            return Ok(attribute); 
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddAttribute([FromBody] BusinessObj.Models.Attribute attribute)
        {
            if (attribute == null)
            {
                return BadRequest("Attribute object is null"); 
            }

            try
            {
                await _service.AddAttribute(attribute);
                return Ok("Attribute added successfully"); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateAttribute(string id, [FromBody] BusinessObj.Models.Attribute attribute)
        {
            if (attribute == null || attribute.AttributeId != id)
            {
                return BadRequest("Attribute data is invalid");
            }

            try
            {
                await _service.UpdateAttribute(attribute);
                return Ok("Attribute updated successfully"); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAttribute(string id)
        {
            try
            {
                await _service.DeleteAttribute(id);
                return Ok("Attribute deleted successfully"); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
