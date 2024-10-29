using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFeedbackService
    {
        public Task<List<Feedback>> GetAllFeedback();
        public Task<Feedback> GetFeedbackByID(string id);
        public Task<string> AddFeedback(Feedback feedback);
        public Task<string> UpdateFeedback(Feedback feedback);
        public Task<string> DeleteFeedback(string id);
    }
}
