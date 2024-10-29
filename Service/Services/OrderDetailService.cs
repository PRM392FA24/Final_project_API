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
    public class OrderDetailService: IOrderDetailService
    {
        private readonly OrderDetailRepository _repo;

        public OrderDetailService(OrderDetailRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> AddOrderDetail(OrderDetail orderDetail)
        {
            return await _repo.CreateAsync(orderDetail);
        }

        public async Task<string> DeleteOrderDetail(string orderId, string productId)
        {
            return await _repo.DeleteAsync(orderId, productId);
        }

        public async Task<List<OrderDetail>> GetAllOrderDetail()
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

        public async Task<OrderDetail> GetOrderDetailByID(string orderId, string productId)
        {
            return await _repo.GetByIdAsync(orderId, productId);
        }

        public async Task<string> UpdateOrderDetail(OrderDetail orderDetail)
        {
            return await _repo.UpdateAsync(orderDetail);
        }
    }
}
