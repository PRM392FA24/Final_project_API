using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEntertainService
    {
        public Task<List<Entertain>> GetAllEntertain();
        public Task<Entertain> GetEntertainByID(string id);
        public Task<string> AddEntertain(Entertain entertain);
        public Task<string> UpdateEntertain(Entertain entertain);
        public Task<string> DeleteEntertain(string id);
    }
}
