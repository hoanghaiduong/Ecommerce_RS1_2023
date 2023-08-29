using Ecommerce_2023.Models;
using Ecommerce_2023.Services;
using Ecommerce_2023.Shared;
using Ecommerce_2023.Shared.Enum;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce_2023.Controllers
{
    [Consumes("application/json")] // Set default request media type
    [Produces("application/json")] // Set default response media type
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly DatabaseContext _context;
        private readonly FirebaseAuth _firebaseAuth;
        private readonly UserService _userService;
        private readonly IWebHostEnvironment _env;
        public AuthController(ILogger<AuthController> logger, FirebaseAuth firebaseAuth, DatabaseContext context, UserService userService, IWebHostEnvironment environment)
        {
            _logger = logger;
            _context = context;
            _firebaseAuth = firebaseAuth;
            _userService = userService;
            _env = environment;
        }

        // POST: AuthController/Create

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> SignUp()
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
                {
                    return Unauthorized();
                }
                string idToken = authorizationHeader.Substring("Bearer ".Length);

                var decodeToken = await _firebaseAuth.VerifyIdTokenAsync(idToken);
                var user = await _firebaseAuth.GetUserAsync(decodeToken.Uid);
                bool? checkUserDatabase = await _userService.CheckUserExistAsync(user.Uid);

                User savingUser;

                if ((bool)!checkUserDatabase)
                {
                    savingUser = new User()
                    {
                        Id = user.Uid,
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.DisplayName,
                        EmailVerified = user.EmailVerified,
                        
                    };
                    await _context.Users.AddAsync(savingUser);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    savingUser = await _userService.GetUserById(user.Uid);
                }

                _logger.LogInformation(checkUserDatabase.ToString());
                Console.WriteLine(checkUserDatabase);

                return new ResponseModel()
                {
                    Message = "Success",
                    Result = savingUser
                };
            }
            catch (FirebaseAuthException ex)
            {
                _logger.LogError(ex, "Error while processing SignUp.");
                return new ResponseModel()
                {
                    Status = false,
                    Result = ex.Message
                };
            }
        }

        [HttpPost("uploadFile")]
        [Consumes("multipart/form-data")]
        public IActionResult UploadFile([FromQuery] EImageType imageType,[FromForm] List<IFormFile> postedFiles)
        {
            string contentRootPath = _env.ContentRootPath;
            string webRootPath = _env.WebRootPath;
            string path="";
            foreach (var file in postedFiles)
            {
                if (file.Length > 0)
                {
                    var uploadDirectory = Path.Combine(contentRootPath, "uploads");

                    // Create the "uploads" directory if it doesn't exist
                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    // Generate a unique filename
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    // Set the file path
                    var filePath = Path.Combine(uploadDirectory, fileName);
                    path = filePath;
                    // Save the file using a FileStream
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
        
            return Ok(new
            {
                Message="Success",
                Result=path,
                custom= imageType.GetDescription()
            });
        }

    }
}
