using BusinessObj.Models;
using DataAccessObj;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{

    public class PromotionRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public PromotionRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<Promotion>> GetAllAsync()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion> GetByIdAsync(string promotionId)
        {
            return await _context.Promotions.FindAsync(promotionId);
        }

        public async Task<string> AddAsync(Promotion promotion)
        {
            try
            {
                var add = new Promotion();
                add.PromotionId = Guid.NewGuid().ToString("N").Substring(0, 10);
                add.PromotionName = promotion.PromotionName;
                add.Description = promotion.Description;
                add.Discount = promotion.Discount;
                add.Quantity = promotion.Quantity;


                add.CreateBy = promotion.CreateBy;
                add.CreateAt = DateTime.Now;

                await this._context.Promotions.AddAsync(add);
                await this._context.SaveChangesAsync();
                return "SUCCESS";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(Promotion promotion)
        {
            try
            {
                var update = await this._context.Promotions.Where(x => x.PromotionId.Equals(promotion.PromotionId))
                                  .FirstOrDefaultAsync();
                if (update != null)
                {
                    update.PromotionId = promotion.PromotionId;
                    update.PromotionName = promotion.PromotionName;
                    update.Description = promotion.Description;
                    update.Discount = promotion.Discount;
                    update.Quantity = promotion.Quantity;

                    update.UpdateBy = promotion.UpdateBy;
                    update.UpdateAt = DateTime.Now;

                    this._context.Promotions.Update(update);
                    await this._context.SaveChangesAsync();
                    return "SUCCESS";

                }
                return "FAIL";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        public async Task<string> DeleteAsync(string promotionId)
        {
            try
            {
                var delete = await this._context.Promotions.Where(x => x.PromotionId.Equals(promotionId))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                    if (delete.Status == false)
                    {
                        throw new Exception("NotFound");
                    }
                    delete.Status = false;
                    this._context.Promotions.Update(delete);
                    await _context.SaveChangesAsync();
                    return "SUCCESS";
                }
                return "FAIL";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
