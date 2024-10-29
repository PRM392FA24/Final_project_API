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
    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackRepository _repo;

        public FeedbackService(FeedbackRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> AddFeedback(Feedback feedback)
        {
            return await _repo.CreateAsync(feedback);
        }

        public async Task<string> DeleteFeedback(string id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<Feedback>> GetAllFeedback()
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

        public async Task<Feedback> GetFeedbackByID(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<string> UpdateFeedback(Feedback feedback)
        {
            return await _repo.UpdateAsync(feedback);
        }
    }
}
