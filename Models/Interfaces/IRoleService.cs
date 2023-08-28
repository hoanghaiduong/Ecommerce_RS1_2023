﻿using Ecommerce_2023.Models.DTO;
using Ecommerce_2023.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_2023.Models.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetEmployeesListAsync();
        Task<Role> GetRoleByIdAsync(int id);
        Task<Role> GetRoleByNameAsync(string roleName);

        Task<ActionResult<ResponseModel>> SaveRoleAsync(RoleDTO role,int? id);
        Task<ResponseModel> DeleteRoleAsync(int id);
    }
}
