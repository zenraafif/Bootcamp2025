using Microsoft.AspNetCore.Mvc;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service) => _service = service;

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetName(int id)
        {
            var name = await _service.GetUserNameAsync(id);
            return Ok(name);
        }
    }
}
