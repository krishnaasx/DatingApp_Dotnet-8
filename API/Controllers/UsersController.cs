using API.DTOs;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers  {

    [ApiController]
    public class UsersController(IUserRespository userRespository) : BaseApiController {

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() {
            var users = await userRespository.GetMembersAsync();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username) {
            var user = await userRespository.GetMemberAsync(username);
            if (user == null) return NotFound();
            return user;
        }
    }
}