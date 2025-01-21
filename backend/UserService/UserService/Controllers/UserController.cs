using Application.Query;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _db;
        public UserController(IUserRepository db)
        {
            _db = db;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _db.GetAllAsync(UserQuery.GetAllUser);
            return Ok(users);
        }
    }
}
