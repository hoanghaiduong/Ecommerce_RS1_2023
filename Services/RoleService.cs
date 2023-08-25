using Ecommerce_2023.Models.Interfaces;
using Ecommerce_2023.Models.Role;
using Ecommerce_2023.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_2023.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleContext roleContext;

        public RoleService(RoleContext context)
        {
            roleContext = context;
        }

        public async Task<ResponseModel> DeleteRoleAsync(int id)
        {
            var role = await roleContext.Roles.FindAsync(id);
            if (role == null)
            {
                return new ResponseModel { Status = false, Message = "Role not found" };
            }

            roleContext.Roles.Remove(role);
            await roleContext.SaveChangesAsync();

            return new ResponseModel { Status = true, Message = $"Role with id: {id} deleted successfully" };
        }

        public async Task<List<RoleEntity>> GetEmployeesListAsync()
        {
            return await roleContext.Roles.ToListAsync();
        }

        public async Task<RoleEntity> GetRoleByIdAsync(int id)
        {
            return await roleContext.Roles.FindAsync(id);
        }

        public async Task<RoleEntity> GetRoleByNameAsync(string roleName)
        {
            return await roleContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<ResponseModel> SaveRoleAsync(RoleEntity roleEntity)
        {
            roleContext.Roles.Add(roleEntity);
            await roleContext.SaveChangesAsync();

            return new ResponseModel { Status = true, Message = "Role saved successfully" };
        }
    }
}
