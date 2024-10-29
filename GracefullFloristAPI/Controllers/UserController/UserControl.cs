using BusinessObj.Models;
using DataAccessObj.DTO;
using DataAccessObj.DTO.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;


namespace GracefullFloristAPI.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControl : ControllerBase
    {
        private readonly IUserService _service;
        public UserControl(IUserService service) 
        {
            _service = service;
        }

        [Route("{ID}")]
        [HttpPost]
        public async Task<IActionResult> GetUserById([FromRoute] string ID)
        { 
            ResponseType<BusinessObj.Models.User> response = new ResponseType<BusinessObj.Models.User>();
            try
            {
                response.Data = await this._service.GetUserInformation(ID);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ResponseType<BusinessObj.Models.User> response = new ResponseType<BusinessObj.Models.User>();
            try
            {
                response.Data = await this._service.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }
        
        [Route("{Name}")]
        [HttpPost]
        public async Task<IActionResult> SearchByName([FromRoute] string Name)
        {
            ResponseType<BusinessObj.Models.User> response = new ResponseType<BusinessObj.Models.User>();
            try
            {
                response.Data = await this._service.SearchByName(Name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUser request)
        {
            ResponseType<BusinessObj.Models.User> response = new ResponseType<BusinessObj.Models.User>();
            try
            {
                response.Data = await this._service.Update(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [Route("Delete/{request}")]
        [HttpDelete]
        public async Task<IActionResult> Remove([FromRoute] string request)
        {
            ResponseType<BusinessObj.Models.User> response = new ResponseType<BusinessObj.Models.User>();
            try
            {
                response.Data = await this._service.Remove(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [Route("Regis")]
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterDTO request)
        {
            ResponseType<BusinessObj.Models.User> response = new ResponseType<BusinessObj.Models.User>();
            try
            {
                response.Data = await this._service.Registration(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            ResponseType<BusinessObj.Models.User> response = new ResponseType<BusinessObj.Models.User>();
            try
            {
                response.Data = await this._service.Login(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser request)
        {
            ResponseType<User> response = new ResponseType<User>();
            try
            {
                response.Data = await this._service.CreateUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [Route("CountCustomer")]
        [HttpGet]
        public async Task<IActionResult> countCustomers()
        {
            ResponseType<BusinessObj.Models.User> response = new ResponseType<BusinessObj.Models.User>();
            try
            {
                response.Data = await this._service.countCustomers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

    }
}
