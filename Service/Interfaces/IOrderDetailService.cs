using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IOrderDetailService
    {
        public Task<List<OrderDetail>> GetAllOrderDetail();
        public Task<OrderDetail> GetOrderDetailByID(string orderId, string productId);
        public Task<string> AddOrderDetail(OrderDetail orderDetail);
        public Task<string> UpdateOrderDetail(OrderDetail orderDetail);
        public Task<string> DeleteOrderDetail(string orderId, string productId);
    }
}
