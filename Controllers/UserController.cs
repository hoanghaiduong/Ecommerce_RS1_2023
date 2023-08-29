using Ecommerce_2023.Models;
using Ecommerce_2023.Shared;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce_2023.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly FirebaseAuth _firebaseAuth;

     
        public UserController(DatabaseContext context,FirebaseAuth firebaseAuth)
        {
            this._context = context;
           
            _firebaseAuth = firebaseAuth;

        }
   
        // POST api/<UserController>
        [HttpPost]
        
        public async Task<ActionResult<ResponseModel>> Post( )
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(); // Return unauthorized status if no or incorrect Authorization header
                }

                string idToken = authorizationHeader.Substring("Bearer ".Length);


                var created = await _firebaseAuth.VerifyIdTokenAsync(idToken);

                return Ok(new ResponseModel()
                {
                    Message = "Đăng ký người dùng thành công!",
                    Status = true,
                    Result = created
                }); 
                
            }
            catch (FirebaseAuthException ex)
            {

                return BadRequest(new ResponseModel()
                {
                    Message = ex.Message,
                    Status = false,
                    Result = ex.Message
                });
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
