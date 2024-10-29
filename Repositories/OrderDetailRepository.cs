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
    public class OrderDetailRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public OrderDetailRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetByIdAsync(string orderId, string productId)
        {
            return await _context.OrderDetails.FindAsync(orderId, productId);
        }

        public async Task<string> CreateAsync(OrderDetail orderDetail)
        {
            try
            {
                var add = new OrderDetail();
                add.OrderId = Guid.NewGuid().ToString("N").Substring(0, 10);
                add.ProductId = Guid.NewGuid().ToString("N").Substring(0, 10);
                add.Order = orderDetail.Order;
                add.Product = orderDetail.Product;
                add.Quantity = orderDetail.Quantity;
                add.Price = orderDetail.Price;

                await this._context.OrderDetails.AddAsync(add);
                await this._context.SaveChangesAsync();
                return "SUCCESS";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(OrderDetail orderDetail)
        {
            try
            {
                var update = await this._context.OrderDetails.Where(x => x.OrderId.Equals(orderDetail.OrderId) &&
                                                                         x.ProductId.Equals(orderDetail.ProductId))
                                  .FirstOrDefaultAsync();
                if (update != null)
                {

                    update.Quantity = orderDetail.Quantity;
                    update.Price = orderDetail.Price;

                    this._context.OrderDetails.Update(update);
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

        public async Task<string> DeleteAsync(string orderId, string productId)
        {
             try
            {
                var delete = await this._context.OrderDetails.Where(x => x.OrderId.Equals(orderId) && 
                                                                         x.ProductId.Equals(productId))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                   
                    this._context.OrderDetails.Remove(delete);
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
