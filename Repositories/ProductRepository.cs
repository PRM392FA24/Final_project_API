using BusinessObj.Models;
using DataAccessObj;
using DataAccessObj.DTO.ProductDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public ProductRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
            => await _context.Products.Where(x => (x.CustomBy != null && x.DeleteAt == null))
                                        .Include(z => z.RefProductAttributes)
                                            .ThenInclude(y => y.Attribute)
                                        .Include(q => q.RefProductImgs)
                                            .ThenInclude(a => a.En)
                                        .ToListAsync();



        public async Task<Product> GetById(string id)
            => await _context.Products.Where(x => x.DeleteAt == null)
                                        .Include(z => z.RefProductAttributes)
                                            .ThenInclude(y => y.Attribute)
                                        .Include(q => q.RefProductImgs)
                                            .ThenInclude(a => a.En)
                                        .Include(f => f.RefFeedbacks)
                                            .ThenInclude(s => s.Feedback)
                                        .FirstOrDefaultAsync();

        public async Task<List<Product>> GetCustomize(string UserID) 
            => await this._context.Products.Where(x => x.CustomBy.Equals(UserID))
                                           .Include(z => z.RefProductAttributes)
                                            .ThenInclude(y => y.Attribute)
                                           .Include(q => q.RefProductImgs)
                                            .ThenInclude(a => a.En)
                                           .Include(f => f.RefFeedbacks)
                                            .ThenInclude(s => s.Feedback)
                                           .ToListAsync();
        public async Task<string> Create(CreateProductDTO product)
        {
            try
            {
                //Product
                var add = new Product();
                add.ProductId = Guid.NewGuid().ToString("P").Substring(0, 10);
                add.ProductName = product.ProductName;
                add.ProdutDesription = product.ProdutDesription;
                add.ProductQuantity = product.ProductQuantity;
                add.Price = product.Price;
                add.CreateBy = product.CreateBy;
                add.CreateAt = DateTime.UtcNow;
                //Attribute
                var temp = new RefProductAttribute();
                foreach (var items in product.Attributes)
                {
                    temp.AttributeId = items;
                    temp.ProductId = add.ProductId;
                    await this._context.RefProductAttributes.AddAsync(temp);
                    await this._context.SaveChangesAsync();
                }


                await this._context.Products.AddAsync(add);
                await this._context.SaveChangesAsync();
                return "SUCCESS";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> CreateCustom(CustomizeProduct product)
        {

            try
            {
                if ((await this._context.Products.Where(x => x.CustomBy.Equals(product.CustomBy)).ToListAsync()).Count() > 3)
                    return "You have had 3 Customize products already. Clear your bag first please!!!";
                //Product
                var add = new Product();
                add.ProductId = Guid.NewGuid().ToString("Cus").Substring(0, 10);
                add.ProductName = product.ProductName;
                add.ProdutDesription = product.ProdutDesription;
                add.ProductQuantity = product.ProductQuantity;
                add.Price = product.Price;
                add.Status = true;
                add.CustomBy = product.CustomBy;
                add.CustomAt = DateTime.Now;
                await this._context.Products.AddAsync(add);
                await this._context.SaveChangesAsync();
                //Attribute
                var temp = new RefProductAttribute();
                foreach (var items in product.Attributes)
                {
                    temp.AttributeId = items.Value;
                    temp.ProductId = add.ProductId;
                    temp.QuantityIn = items.Key;
                    await this._context.RefProductAttributes.AddAsync(temp);
                    await this._context.SaveChangesAsync();
                }



                return "SUCCESS";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> Update(UpdateProductDTO product)
        {
            try
            {
                var update = await this._context.Products.Where(x => x.ProductId.Equals(product.ProductId))
                                  .FirstOrDefaultAsync();
                if (update != null)
                {
                    update.ProductId = product.ProductId ?? update.ProductId;
                    update.ProductName = product.ProductName ?? update.ProductName;
                    update.ProdutDesription = product.ProdutDesription ?? update.ProdutDesription;
                    if (product.ProductQuantity == null)
                    {
                        update.ProductQuantity = update.ProductQuantity;
                    }
                    else
                    {
                        update.ProductQuantity = product.ProductQuantity;
                    }
                    if (product.Price == null)
                    {
                        update.Price = update.Price;
                    }
                    else
                    {
                        update.Price = product.Price;
                    }

                    update.UpdateBy = product.UserID;
                    update.UpdateAt = DateTime.UtcNow;

                    this._context.Products.Update(update);
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

        public async Task<string> Delete(DeleteProductDTO request)
        {
            try
            {
                var delete = await this._context.Products.Where(x => x.ProductId.Equals(request.productID))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                    if (delete.Status == false)
                    {
                        throw new Exception("NotFound");
                    }
                    delete.Status = false;
                    delete.DeleteAt = DateTime.UtcNow;
                    delete.DeleteBy = request.UserID;
                    this._context.Products.Update(delete);
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
        public async Task<string> Remove(string ID)
        {
            try
            {
                this._context.Products.Remove(await this._context.Products.Where(x => x.ProductId.Equals(ID) && x.CustomBy != null).FirstOrDefaultAsync());
                await this._context.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not remove {ex.Message}");
            }
        }
    }
}
