using Ecommerce_2023.Models;
using Ecommerce_2023.Models.DTO;
using Ecommerce_2023.Models.Interfaces;
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
        private readonly DatabaseContext _db;

        public RoleService(DatabaseContext context)
        {
            _db = context;
        }

        public async Task<ResponseModel> DeleteRoleAsync(int id)
        {
            var role = await _db.Roles.FindAsync(id);
            if (role == null)
            {
                return new ResponseModel { Status = false, Message = "Role not found" };
            }

            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();

            return new ResponseModel { Status = true, Message = $"Role with id: {id} deleted successfully" };
        }

        public async Task<List<Role>> GetEmployeesListAsync()
        {
            return await _db.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _db.Roles.FindAsync(id);
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<ActionResult<ResponseModel>> SaveRoleAsync(RoleDTO roleDTO, int? id = null)
        {
            Role role;
            if (id == null)
            {
                role = new Role()
                {
                    Name = roleDTO.Name,
                    IsActive = roleDTO.IsActive
                };
                _db.Roles.Add(role);
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
              
                await _db.SaveChangesAsync();

                return new ResponseModel { Status = true, Message = "Role saved successfully", Result = role };
        }
        //private ResponseModel CreatedAtActionResult(string v, object value, RoleEntity role)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
