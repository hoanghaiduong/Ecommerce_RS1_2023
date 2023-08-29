using Ecommerce_2023.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Ecommerce_2023.Services
{
    public class UserService
    {
        private readonly DatabaseContext _context;
        public UserService(DatabaseContext context) {
        
            this._context= context;
        }
        public async Task<User> GetUserById(string id)
        {
            try
            {
                var user = await this._context.Users.SingleOrDefaultAsync(u => u.Id == id);
                return user; // Trả về người dùng duy nhất hoặc null
            }
            catch (DbException ex)
            {
                // Thay vì ném ngoại lệ, bạn có thể trả về null hoặc giá trị mặc định
                return null;
            }
        }

        public async Task<bool?> CheckUserExistAsync(string userId)
        {
            try
            {
                bool check = await _context.Users.AnyAsync(o => o.Id == userId);
                return check;
            }
            catch 
            {
           
                return null;
            }
        }

    }
}
