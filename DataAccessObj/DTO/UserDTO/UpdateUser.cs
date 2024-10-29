using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObj.DTO.UserDTO
{
    public class UpdateUser
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public string Img_url {  get; set; }
        public int RoleId { get; set; }
    }
}
