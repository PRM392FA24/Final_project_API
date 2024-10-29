using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPromotionService
    {
        public Task<List<Promotion>> GetAllPromotion();
        public Task<Promotion> GetPromotionByID(string id);
        public Task<string> AddPromotion(Promotion promotion);
        public Task<string> UpdatePromotion(Promotion promotion);
        public Task<string> DeletePromotion(string id);
    }
}
