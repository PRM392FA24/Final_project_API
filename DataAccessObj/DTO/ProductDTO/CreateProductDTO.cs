using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObj.DTO.ProductDTO
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; }
        public string ProdutDesription { get; set; }
        public int ProductQuantity { get; set; }
        public double Price { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public bool? Status { get; set; }
        public List<string> Attributes { get; set; }
    }
}
