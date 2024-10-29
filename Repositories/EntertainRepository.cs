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
    public class EntertainRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public EntertainRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<Entertain>> GetAllAsync()
        {
            return await _context.Entertains.ToListAsync();
        }

        public async Task<Entertain> GetByIdAsync(string id)
        {
            return await _context.Entertains.FindAsync(id);
        }

        public async Task<string> CreateAsync(Entertain entertain)
        {
            try
            {
                    var add = new Entertain();
                    add.EnId = Guid.NewGuid().ToString("N").Substring(0, 10);
                    add.Status = true;
                    add.ImgUrl = entertain.ImgUrl;

                    await this._context.Entertains.AddAsync(add);
                    await this._context.SaveChangesAsync();
                    return "SUCCESS";
                
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<string> UpdateAsync(Entertain entertain)
        {
            try
            {
                var update = await this._context.Entertains.Where(x => x.EnId.Equals(entertain.EnId))
                                  .FirstOrDefaultAsync();
                if (update != null)
                {

                    update.Status = entertain.Status;
                    update.ImgUrl = entertain.ImgUrl ?? update.ImgUrl;

                    this._context.Entertains.Update(update);
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

        public async Task<string> DeleteAsync(string id)
        {
            try
            {
                var delete = await this._context.Entertains.Where(x => x.EnId.Equals(id))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                    if (delete.Status == false)
                    {
                        throw new Exception("NotFound");
                    }
                    delete.Status = false;
                    this._context.Entertains.Update(delete);
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
