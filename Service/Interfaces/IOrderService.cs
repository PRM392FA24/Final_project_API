using BusinessObj.Models;
using DataAccessObj.DTO.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IOrderService
    {
        public Task<List<Order>> GetAllOrder();
        public Task<Order> GetOrderByID(string id);
        public Task<string> AddOrder(Order order);
        public Task<string> UpdateOrder(Order order);
        public Task<string> DeleteOrder(DeleteOrderDTO request);
    }
}
