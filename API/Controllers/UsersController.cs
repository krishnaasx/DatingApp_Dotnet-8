using System.Security.Claims;
using API.DTOs;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers  {

    [Authorize]
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

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto) {

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (username == null) return BadRequest("No username found in token");
            var user = await userRespository.GetUserByUsernameAsync(username);
            if(user == null) return BadRequest("Count not find the user");
            mapper.Map(memberUpdateDto, user);
            if (await userRespository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update the user");
        }
    }
}