using BusinessObj.Models;
using DataAccessObj.DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetCustomize(string UserID);
        public Task<List<Product>> GetAllProduct();
        public Task<Product> GetProductByID(string id);
        public Task<string> AddProduct(CreateProductDTO product);
        public Task<string> UpdateProduct(UpdateProductDTO request);
        public Task<string> DeleteProduct(DeleteProductDTO request);
    }

}
