using BusinessObj.Models;
using DataAccessObj.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAll();
        public Task<User> GetUserInformation(string user);
        public Task<List<User>> SearchByName(string FullName);
        public Task<User> Update(UpdateUser user);
        public Task<bool> Remove(string userID);
        public Task<User> Registration(RegisterDTO request);
        public Task<string> Login(LoginDTO request);
        public Task<User> CreateUser(CreateUser user);
        public Task<Int32> countCustomers();
    }
}
