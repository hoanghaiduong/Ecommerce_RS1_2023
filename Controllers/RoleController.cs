using Ecommerce_2023.Models.Role;
using Ecommerce_2023.Models.Roles.DTO;
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
        private readonly ILogger<RoleController> _logger;
        private readonly RoleContext _dbContext;
        public RoleController(RoleContext dbContext) {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            if(_dbContext.Roles==null)
            {
                return NotFound(new
                {
                    Message="Không tìm thấy bất kì dữ liệu nào",
                    StatusCode=HttpStatusCode.NotFound
                });
            }    
            return await _dbContext.Roles.ToListAsync();
        }
        [HttpGet]
        [Route("getById")]
        public async Task<ActionResult<Role>> GetRole([FromQuery] int id)
        {
            if (_dbContext.Roles == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy bất kì dữ liệu nào",
                    StatusCode = HttpStatusCode.NotFound
                });
            }
            var role = await _dbContext.Roles.FindAsync(id);
            if (role==null)
            {
                return NotFound();
            }
            return role;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
           _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateRole), new {id=role.Id},role);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<Role>> UpdateRole([FromQuery] int id, [FromBody] RoleDTO role)
        {
          

            var existingRole = await _dbContext.Roles.FindAsync(id);
            if (existingRole == null)
            {
                return NotFound("Id in the query parameters does not match the Id in the Role object");
            }
          
           _dbContext.Entry(existingRole).CurrentValues.SetValues(role);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("An error occurred while updating the Role", ex);
            }

            return Ok(new
            {
                message=$"Update data with id :{id} successfully",
                data=existingRole
            });
        }

        private bool CheckRoleExists(int id)
        {
            return (_dbContext.Roles?.Any(r => r.Id == id)).GetValueOrDefault();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> DeleteRole([FromQuery] int id)
        {


            var existingRole = await _dbContext.Roles.FindAsync(id);
            if (existingRole == null)
            {
                return NotFound("Id in the query parameters does not match the Id in the Role object");
            }

            _dbContext.Remove(existingRole);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("An error occurred while updating the Role", ex);
            }

            return Ok(new
            {
                message = $"Delete data with id :{id} successfully",
               
            });
        }
    }
}
