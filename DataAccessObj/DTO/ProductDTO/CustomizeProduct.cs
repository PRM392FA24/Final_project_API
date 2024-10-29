using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObj.DTO.ProductDTO
{
    public class CustomizeProduct
    {
        public string ProductName { get; set; }
        public string ProdutDesription { get; set; }
        public int ProductQuantity { get; set; }
        public double Price { get; set; }
        public string CustomBy { get; set; }
        public DateTime? CustomAt { get; set; }
        public bool? Status { get; set; }
        public Dictionary<int ,string> Attributes { get; set; }
    }
}
