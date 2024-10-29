using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITransactionService
    {
        public Task<List<Transaction>> GetAllTransaction();
        public Task<Transaction> GetTransactionByID(string id);
        public Task<string> AddTransaction(Transaction transaction);
        public Task<string> UpdateTransaction(Transaction transaction);
        public Task<string> DeleteTransaction(string id);
    }
}
