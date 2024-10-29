using DataAccessObj;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repositories
{
    public class AttributeRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public AttributeRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<BusinessObj.Models.Attribute>> GetAllAttribute()
        {
            return await _context.Attributes.ToListAsync();
        }

        public async Task<BusinessObj.Models.Attribute> GetAttributeById(string id)
        {
            return await _context.Attributes.FindAsync(id);
        }

        public async Task<string> CreateAttribute(BusinessObj.Models.Attribute attribute)
        {
            try
            {
               
                    var add = new BusinessObj.Models.Attribute();
                    add.AttributeId = Guid.NewGuid().ToString("N").Substring(0, 10);
                    add.Name = attribute.Name;
                    add.Status = true;
                    add.Quantity = attribute.Quantity;
                    add.Price = attribute.Price;
                    add.Desription = attribute.Desription;
                    add.Type = attribute.Type;
                    add.ImgUrl = attribute.ImgUrl;
                    
                    add.CreateBy = attribute.CreateBy;
                    add.CreateAt = DateTime.Now;
                    await this._context.Attributes.AddAsync(add);
                    await this._context.SaveChangesAsync();
                    return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateAttribute(BusinessObj.Models.Attribute attribute)
        {
            try
            {
                var update = await this._context.Attributes.Where(x => x.AttributeId.Equals(attribute.AttributeId))
                                    .FirstOrDefaultAsync();
                if (update != null)
                {
                    update.Name = attribute.Name ?? update.Name;
                    update.Status = attribute.Status ?? update.Status;
                    update.Quantity = attribute.Quantity;
                    update.Price = attribute.Price;
                    update.Desription = attribute.Desription ?? update.Desription;
                    update.Type = attribute.Type;
                    update.ImgUrl = attribute.ImgUrl ?? update.ImgUrl;

                    update.UpdateBy = attribute.UpdateBy;
                    update.UpdateAt = DateTime.Now;
                    this._context.Attributes.Update(update);
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

        public async Task<string> DeleteAsync(string id)
        {
            try
            {
                var delete = await this._context.Attributes.Where(x => x.AttributeId.Equals(id))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                    if (delete.Status == false)
                    {
                        throw new Exception("NotFound");
                    }
                    delete.Status = false;
                    delete.DeleteBy = delete.DeleteBy;
                    delete.DeleteAt = DateTime.Now;
                    this._context.Attributes.Update(delete);
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
