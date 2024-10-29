using BusinessObj.Models;
using DataAccessObj.DTO.UserDTO;
using DataAccessObj;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly GRACEFULLFLORISTContext _context;
        public UserRepository(IConfiguration configuration, GRACEFULLFLORISTContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public UserRepository() { }
        public async Task<List<User>> GetAll()
        {
            try
            {
                var list = await this._context.Users.Include(x => x.Role).ToListAsync();
                if (list != null)
                {
                    return list;
                }
                throw new Exception("There are no USER");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<User> GetUserInformation(string user)
        {
            try
            {
                var search = await this._context.Users.Where(x => x.UserId.Equals(user))
                                                        .Include(x => x.Role)       
                                                            .FirstOrDefaultAsync();
                return search;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<List<User>> SearchByName(string FullName)
        {
            try
            {
                var list = await this._context.Users.Where(x => x.Fullname.Contains(FullName)).Include(x => x.Role).ToListAsync();
                if (list != null) return list;
                throw new Exception("Not Found");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<User> Update(UpdateUser user)
        {
            try
            {
                var r = await this._context.Users.Where(x => user.UserID.Equals(x.UserId))
                                                .FirstOrDefaultAsync();
                if (user != null && r != null)
                {
                    r.Fullname = user.Fullname ?? r.Fullname;
                    r.Address = user.Address ?? r.Address;
                    r.Email = user.Email ?? r.Email;
                    r.Phonenumber = user.Phonenumber ?? r.Phonenumber;
                    r.RoleId = user.RoleId;
                    r.ImgUrl = user.Img_url ?? r.ImgUrl;
                    r.Passwork = user.Password ?? r.Passwork;
                    this._context.Users.Update(r);
                    await this._context.SaveChangesAsync();
                    return r;
                }
                return r;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> Remove(string userID)
        {
            if (userID != null)
            {
                var obj = await this._context.Users.Where(x => x.UserId.Equals(userID)).FirstOrDefaultAsync();
                obj.Status = false;
                this._context.Users.Update(obj);
                await this._context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, user.Role.RoleId.ToString()),
                new Claim("userid", user.UserId),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }

        public async Task<User> Registration(RegisterDTO request)
        {
            try
            {
                var r = new User();
                if (request != null)
                {
                    var test = await this._context.Users.Where(x => x.Username.Equals(request.Username)).FirstOrDefaultAsync();
                        if ( test != null)
                        {
                            throw new Exception("Duplicate UserName");
                        }
                    
                    r.UserId = "US" + Guid.NewGuid().ToString().Substring(0, 10);
                    r.Username = request.Username;
                    r.Fullname = request.Fullname;
                    r.Passwork = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    r.Email = request.Email;
                    r.Address = request.Address ?? null;
                    r.CreateDate = DateTime.UtcNow;
                    r.Phonenumber = request.Phonenumber ?? null;
                    r.RoleId = 1;
                    r.Status = true;
                    await this._context.Users.AddAsync(r);
                    await this._context.SaveChangesAsync();
                    return r;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<string> Login(LoginDTO request)
        {
            try
            {
                var user = await this._context.Users.Where(x => x.Username.Equals(request.Username))
                                                   .Include(y => y.Role)
                                                   .FirstOrDefaultAsync();
                if (user == null)
                    throw new Exception("USER IS NOT FOUND");
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Passwork))
                    throw new Exception("INVALID PASSWORD");
                if (!user.Status)
                    throw new Exception("ACCOUNT WAS BANNED OR DELETED");
                return CreateToken(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<User> CreateUser(CreateUser user)
        {
            try
            {
                var userS = await this._context.Users.Where(x => x.Equals(user.Username)).FirstOrDefaultAsync();
                if (userS != null)
                    throw new Exception("Duplicate Username Please try another onr.");
                var create = new User();
                create.UserId = "US" + Guid.NewGuid().ToString().Substring(0, 7);
                create.RoleId = user.RoleId;
                create.Username = user.Username;
                create.Passwork = BCrypt.Net.BCrypt.HashPassword(user.Password);
                create.Fullname = user.Fullname;
                create.Address = user.Address ?? null;
                create.Phonenumber = user.Phonenumber ?? null;
                create.Email = user.Email;
                create.Status = true;
                create.CreateDate = DateTime.UtcNow;
                await this._context.Users.AddAsync(create);
                await this._context.SaveChangesAsync();
                return create;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Int32> countCustomers()
        {
            try
            {
                var list = await this._context.Users.Where(x => x.RoleId.Equals(1)).ToListAsync();
                return list.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
