using BusinessObj.Models;
using DataAccessObj.DTO.ProductDTO;
using Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repo;

        public ProductService(ProductRepository repo)
        {
            this._repo = repo;
        }

        public async Task<string> AddProduct(CreateProductDTO product)
            => await this._repo.Create(product);
        

        public async Task<string> DeleteProduct(DeleteProductDTO request)
            => await this._repo.Delete(request);


        public async Task<List<Product>> GetAllProduct()
            => await this._repo.GetAll();

        public async Task<List<Product>> GetCustomize(string UserID)
            => await this._repo.GetCustomize(UserID);
             

        public async Task<Product> GetProductByID(string id)
            => await this._repo.GetById(id);
        

        public async Task<string> UpdateProduct(UpdateProductDTO request)
            => await this._repo.Update(request);
        
    }
}
