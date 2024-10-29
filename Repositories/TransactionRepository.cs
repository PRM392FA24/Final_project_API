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
    

    public class TransactionRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public TransactionRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(string transactionId)
        {
            return await _context.Transactions.FindAsync(transactionId);
        }

        public async Task<string> AddAsync(Transaction transaction)
        {
            try
            {
               

                var add = new Transaction();
                add.TransactionId = Guid.NewGuid().ToString("N").Substring(0,10);
                add.OrderId = transaction.OrderId;
                add.Order = transaction.Order;
                add.Datetime = transaction.Datetime;
                add.CreateDate = DateTime.Now;
                add.VpnTransDate = transaction.VpnTransDate;
                add.TransactionCode = transaction.TransactionCode;

                await this._context.Transactions.AddAsync(add);
                await this._context.SaveChangesAsync();
                return "SUCCESS";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(Transaction transaction)
        {
            try
            {
                var update = await this._context.Transactions.Where(x => x.TransactionId.Equals(transaction.TransactionId))
                                  .FirstOrDefaultAsync();
                if (update != null)
                {
                    update.TransactionId = transaction.TransactionId;
                    update.OrderId = transaction.OrderId;
                    update.Order = transaction.Order;
                    update.Datetime = transaction.Datetime;
                    update.CreateDate = DateTime.Now;
                    update.VpnTransDate = transaction.VpnTransDate;
                    update.TransactionCode = transaction.TransactionCode;

                    this._context.Transactions.Update(update);
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

        public async Task<string> DeleteAsync(string transactionId)
        {
            try
            {
                var delete = await this._context.Transactions.Where(x => x.TransactionId.Equals(transactionId))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                    this._context.Transactions.Remove(delete);
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
