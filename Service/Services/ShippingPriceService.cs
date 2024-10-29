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
    public class ShippingPriceService : IShippingPriceService
    {
        private readonly ShippingPriceRepository _repo;

        public ShippingPriceService(ShippingPriceRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> AddShippingPrice(ShippingPrice shippingPrice)
        {
            return await _repo.AddAsync(shippingPrice);
        }

        public async Task<string> DeleteShippingPrice(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<ShippingPrice>> GetAllShippingPrice()
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

        public async Task<ShippingPrice> GetShippingPriceByID(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<string> UpdateShippingPrice(ShippingPrice shippingPrice)
        {
            return await _repo.UpdateAsync(shippingPrice);
        }
    }
}
