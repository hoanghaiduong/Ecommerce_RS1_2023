using Ecommerce_2023.Models.Interfaces;
using Ecommerce_2023.Models.Role;
using Ecommerce_2023.Models.Roles.DTO;
using Ecommerce_2023.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        public async Task<ActionResult<ResponseModel>> SaveRoleAsync(RoleDTO roleDTO, int? id = null)
        {
            RoleEntity role;
            if (id == null)
            {
                role = new RoleEntity
                {
                    Name = roleDTO.Name,
                    IsActive = roleDTO.IsActive
                };
                roleContext.Roles.Add(role);
            }
            else
            {
               role = await GetRoleByIdAsync(id.Value);
                if (role != null)
                {
                    role.Name = roleDTO.Name;
                    role.IsActive = roleDTO.IsActive;
                    // Update other properties as needed
                }
            }    
              
                await roleContext.SaveChangesAsync();

                return new ResponseModel { Status = true, Message = "Role saved successfully", Result = role };
        }
        //private ResponseModel CreatedAtActionResult(string v, object value, RoleEntity role)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
