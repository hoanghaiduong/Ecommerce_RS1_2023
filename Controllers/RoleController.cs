﻿using Ecommerce_2023.Models.Interfaces;
using Ecommerce_2023.Models.Role;
using Ecommerce_2023.Models.Roles.DTO;
using Ecommerce_2023.Services;
using Ecommerce_2023.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Ecommerce_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService roleService;

        public RoleController(RoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleEntity>>> GetEmployeesList()
        {
            var roles = await roleService.GetEmployeesListAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleEntity>> GetRoleById(int id)
        {
            var role = await roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> SaveRole(RoleEntity roleEntity)
        {
            var response = await roleService.SaveRoleAsync(roleEntity);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel>> DeleteRole(int id)
        {
            var response = await roleService.DeleteRoleAsync(id);
            return Ok(response);
        }
        //private readonly ILogger<RoleController> _logger;
        //private readonly RoleContext _dbContext;
        //public RoleController(RoleContext dbContext, ILogger<RoleController> logger)
        //{
        //    _dbContext = dbContext;
        //    _logger = logger;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<RoleEntity>>> GetRoles()
        //{
        //    if (_dbContext.Roles == null)
        //    {
        //        return NotFound(new
        //        {
        //            Message = "Không tìm thấy bất kì dữ liệu nào",
        //            StatusCode = HttpStatusCode.NotFound
        //        });
        //    }
        //    return await _dbContext.Roles.ToListAsync();
        //}
        //[HttpGet]
        //[Route("getById")]
        //public async Task<ActionResult<RoleEntity>> GetRole([FromQuery] int id)
        //{
        //    if (_dbContext.Roles == null)
        //    {
        //        return NotFound(new
        //        {
        //            Message = "Không tìm thấy bất kì dữ liệu nào",
        //            StatusCode = HttpStatusCode.NotFound
        //        });
        //    }
        //    var role = await _dbContext.Roles.FindAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    return role;
        //}

        //[HttpPost]
        //[Route("create")]
        //public async Task<ActionResult<RoleEntity>> CreateRole([FromBody] RoleEntity role)
        //{
        //    _dbContext.Roles.Add(role);
        //    await _dbContext.SaveChangesAsync();
        //    return CreatedAtAction(nameof(CreateRole), new { id = role.Id }, role);
        //}

        //[HttpPut]
        //[Route("update")]
        //public async Task<ActionResult<RoleEntity>> UpdateRole([FromQuery] int id, [FromBody] RoleDTO role)
        //{
        //    var existingRole = await _dbContext.Roles.FindAsync(id);
        //    if (existingRole == null)
        //    {
        //        return NotFound("Id in the query parameters does not match the Id in the Role object");
        //    }

        //    _dbContext.Entry(existingRole).CurrentValues.SetValues(role);

        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        throw new Exception("An error occurred while updating the Role", ex);
        //    }

        //    return Ok(new
        //    {
        //        message = $"Update data with id :{id} successfully",
        //        data = existingRole
        //    });
        //}

        //private bool CheckRoleExists(int id)
        //{
        //    return (_dbContext.Roles?.Any(r => r.Id == id)).GetValueOrDefault();
        //}

        //[HttpDelete]
        //[Route("delete")]
        //public async Task<ActionResult> DeleteRole([FromQuery] int id)
        //{


        //    var existingRole = await _dbContext.Roles.FindAsync(id);
        //    if (existingRole == null)
        //    {
        //        return NotFound("Id in the query parameters does not match the Id in the Role object");
        //    }

        //    _dbContext.Remove(existingRole);

        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        throw new Exception("An error occurred while updating the Role", ex);
        //    }

        //    return Ok(new
        //    {
        //        message = $"Delete data with id :{id} successfully",

        //    });
        //}
    }
}
