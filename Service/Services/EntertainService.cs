using BusinessObj.Models;
using Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EntertainService : IEntertainService
    {
        private readonly EntertainRepository _repo;

        public EntertainService(EntertainRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> AddEntertain(Entertain entertain)
        {
            return await _repo.CreateAsync(entertain);
        }

        public async Task<string> DeleteEntertain(string id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<Entertain>> GetAllEntertain()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching all attributes: {ex.Message}", ex);
            }
        }

        public async Task<Entertain> GetEntertainByID(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<string> UpdateEntertain(Entertain entertain)
        {
            return await _repo.UpdateAsync(entertain);
        }
    }
}
