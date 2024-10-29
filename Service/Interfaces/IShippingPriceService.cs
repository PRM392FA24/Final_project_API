using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShippingPriceService
    {
        public Task<List<ShippingPrice>> GetAllShippingPrice();
        public Task<ShippingPrice> GetShippingPriceByID(int id);
        public Task<string> AddShippingPrice(ShippingPrice shippingPrice);
        public Task<string> UpdateShippingPrice(ShippingPrice shippingPrice);
        public Task<string> DeleteShippingPrice(int id);
    }
}
