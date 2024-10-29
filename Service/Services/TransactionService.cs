using BusinessObj.Models;
using Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _repo;

        public TransactionService(TransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> AddTransaction(Transaction transaction)
        {
            return await _repo.AddAsync(transaction);
        }

        public async Task<string> DeleteTransaction(string id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<Transaction>> GetAllTransaction()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching all attributes: {ex.Message}", ex);
            }
        }

        public async Task<Transaction> GetTransactionByID(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<string> UpdateTransaction(Transaction transaction)
        {
            return await _repo.UpdateAsync(transaction);
        }
    }
}
