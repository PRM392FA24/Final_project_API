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
    public class PromotionService : IPromotionService
    {
        private readonly PromotionRepository _repo;

        public PromotionService(PromotionRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> AddPromotion(Promotion promotion)
        {
            return await _repo.AddAsync(promotion);
        }

        public async Task<string> DeletePromotion(string id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<Promotion>> GetAllPromotion()
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

        public async Task<Promotion> GetPromotionByID(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<string> UpdatePromotion(Promotion promotion)
        {
            return await _repo.UpdateAsync(promotion);
        }
    }
}
