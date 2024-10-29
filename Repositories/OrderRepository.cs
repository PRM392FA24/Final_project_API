using BusinessObj.Models;
using DataAccessObj;
using DataAccessObj.DTO.OrderDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public OrderRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<string> CreateAsync(Order order)
        {
            try
            {
                var add = new Order();
              
                add.OrderId = Guid.NewGuid().ToString("N").Substring(0, 10);
                add.Status = order.Status;
                add.User = order.User;
                add.Address = order.Address;
                add.UserId = order.UserId;
                add.Address = order.Address;
                add.Transactions = order.Transactions;
                add.PromotionId = order.PromotionId;
                add.Promotion = order.Promotion;
                add.Total = order.Total;
                add.FullName = order.FullName;
                add.Phonenumber = order.Phonenumber;
                add.IsPayed = order.IsPayed;
                add.PaymentType = order.PaymentType;
                add.Status = 0;
                add.LocationId = order.LocationId;

                add.CreateBy = order.CreateBy;
                add.CreateAt = DateTime.Now;

                await this._context.Orders.AddAsync(add);
                await this._context.SaveChangesAsync();
                return "SUCCESS";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(Order order)
        {
            try
            {
                var update = await this._context.Orders.Where(x => x.OrderId.Equals(order.OrderId))
                                  .FirstOrDefaultAsync();
                if (update != null)
                {

                    update.OrderId = update.OrderId ;
                    update.Status = order.Status;
                    update.User = order.User;
                    update.Address = order.Address;
                    update.UserId = order.UserId;
                    update.Address = order.Address;
                    update.Transactions = order.Transactions;
                    update.PromotionId = order.PromotionId;
                    update.Promotion = order.Promotion;
                    update.Total = order.Total;
                    update.FullName = order.FullName;
                    update.Phonenumber = order.Phonenumber;
                    update.IsPayed = order.IsPayed;
                    update.PaymentType = order.PaymentType;
                    update.Status = 0;
                    update.LocationId = order.LocationId;

                    update.UpdateBy = order.UpdateBy;
                    update.UpdateAt = DateTime.Now;
                    this._context.Orders.Update(update);
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

        public async Task<string> DeleteAsync(DeleteOrderDTO request)
        {
            try
            {
                var delete = await this._context.Orders.Where(x => x.OrderId.Equals(request.Id))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                    delete.DeleteAt = DateTime.UtcNow;
                    delete.DeleteBy = request.UserID;
                    this._context.Orders.Update(delete);
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
