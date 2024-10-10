using API.DTOs;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers  {

    [ApiController]
    public class UsersController(IUserRespository userRespository, IMapper mapper) : BaseApiController {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() {
            var users = await userRespository.GetMembersAsync();
            var usersToRetrun = mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersToRetrun);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username) {
            var user = await userRespository.GetUserByUsernameAsync(username);
            if (user == null) return NotFound();
            return mapper.Map<MemberDto>(user);
        }
    }
}