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
   

    public class ShippingPriceRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public ShippingPriceRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<ShippingPrice>> GetAllAsync()
        {
            return await _context.ShippingPrices.ToListAsync();
        }

        public async Task<ShippingPrice> GetByIdAsync(int shippingId)
        {
            return await _context.ShippingPrices.FindAsync(shippingId);
        }

        public async Task<string> AddAsync(ShippingPrice shippingPrice)
        {
            try
            {
                int maxShippingId = await _context.ShippingPrices.MaxAsync(sp => (int?)sp.ShippingId) ?? 0;

                var add = new ShippingPrice();
                add.ShippingId = maxShippingId + 1;
                add.ShippingPrice1 = shippingPrice.ShippingPrice1;
                add.LocationName = shippingPrice.LocationName;
              


                add.CreateBy = shippingPrice.CreateBy;
                add.CreateAt = DateTime.Now;

                await this._context.ShippingPrices.AddAsync(add);
                await this._context.SaveChangesAsync();
                return "SUCCESS";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(ShippingPrice shippingPrice)
        {
            try
            {
                var update = await this._context.ShippingPrices.Where(x => x.ShippingId.Equals(shippingPrice.ShippingId))
                                  .FirstOrDefaultAsync();
                if (update != null)
                {
                    update.ShippingId = shippingPrice.ShippingId;
                    update.ShippingPrice1 = shippingPrice.ShippingPrice1;
                    update.LocationName = shippingPrice.LocationName;


                    update.UpdateBy = shippingPrice.UpdateBy;
                    update.UpdateAt = DateTime.Now;

                    this._context.ShippingPrices.Update(update);
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

        public async Task<string> DeleteAsync(int shippingId)
        {
            try
            {
                var delete = await this._context.ShippingPrices.Where(x => x.ShippingId.Equals(shippingId))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                    if (delete.Status == false)
                    {
                        throw new Exception("NotFound");
                    }
                    delete.Status = false;
                    this._context.ShippingPrices.Update(delete);
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

        public async Task<Task<string>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
