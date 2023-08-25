﻿using Ecommerce_2023.Models.Role;
using Ecommerce_2023.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_2023.Models.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleEntity>> GetEmployeesListAsync();
        Task<RoleEntity> GetRoleByIdAsync(int id);
        Task<RoleEntity> GetRoleByNameAsync(string roleName);

        Task<ResponseModel> SaveRoleAsync(RoleEntity roleEntity);
        Task<ResponseModel> DeleteRoleAsync(int id);
    }
}