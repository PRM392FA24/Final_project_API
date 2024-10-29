using Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly AttributeRepository _repo;

        public AttributeService(AttributeRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> AddAttribute(BusinessObj.Models.Attribute attribute)
        {
            return await _repo.CreateAttribute(attribute);
        }

        public async Task<string> DeleteAttribute(string id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<BusinessObj.Models.Attribute>> GetAllAttribute()
        {
            try
            {
                return await _repo.GetAllAttribute();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching all attributes: {ex.Message}", ex);
            }
        }

        public async Task<BusinessObj.Models.Attribute> GetAttributeByID(string id)
        {
            return await _repo.GetAttributeById(id);
        }

        public async Task<string> UpdateAttribute(BusinessObj.Models.Attribute attribute)
        {
            return await _repo.UpdateAttribute(attribute);
        }
    }
}
