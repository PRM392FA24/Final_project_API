using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAttributeService
    {
        public Task<List<BusinessObj.Models.Attribute>> GetAllAttribute();
        public Task<BusinessObj.Models.Attribute> GetAttributeByID(string id);
        public Task<string> AddAttribute(BusinessObj.Models.Attribute attribute);
        public Task<string> UpdateAttribute(BusinessObj.Models.Attribute attribute);
        public Task<string> DeleteAttribute(string id);
    }
}
