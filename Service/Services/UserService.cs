using BusinessObj.Models;
using DataAccessObj.DTO.UserDTO;
using Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> countCustomers()
        => await _repo.countCustomers();

        public async Task<User> CreateUser(CreateUser user)
        => await _repo.CreateUser(user);

        public async Task<List<User>> GetAll()
        => await _repo.GetAll();

        public async Task<User> GetUserInformation(string user)
        => await _repo.GetUserInformation(user);

        public async Task<string> Login(LoginDTO request)
        => await _repo.Login(request);

        public async Task<User> Registration(RegisterDTO request)
        => await _repo.Registration(request);

        public async Task<bool> Remove(string userID)
        => await _repo.Remove(userID);

        public async Task<List<User>> SearchByName(string FullName)
        => await _repo.SearchByName(FullName);

        public async Task<User> Update(UpdateUser user)
        => await _repo.Update(user);
    }
}
