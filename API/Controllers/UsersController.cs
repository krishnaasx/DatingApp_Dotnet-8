using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    

    [Authorize]
    public class UsersController(IUserRespository userRespository, IMapper mapper, IPhotoService photoService) : BaseApiController {

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

            var user = await userRespository.GetUserByUsernameAsync(User.GetUsername());
            if(user == null) return BadRequest("Count not find the user");
            mapper.Map(memberUpdateDto, user);
            if (await userRespository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update the user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file) {

            var user = await userRespository.GetUserByUsernameAsync(User.GetUsername());
            if(user == null) return BadRequest("Cannot update user");
            var result = await photoService.AddPhotoAsync(file);
            if(result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            user.Photos.Add(photo);
            
            if (await userRespository.SaveAllAsync()) 
                return CreatedAtAction(nameof(GetUser), 
                    new {username=user.UserName}, mapper.Map<PhotoDto>(photo));

            return BadRequest("Problem adding photo");
        }

        [HttpPut("set-main-photo/{photoId:int}")]
        public async Task<ActionResult> SetMainPhoto(int photoId) {
            var user = await userRespository.GetUserByUsernameAsync(User.GetUsername());
            if (user == null) return BadRequest("Count not find the user");
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null || photo.IsMain) return BadRequest("Cannot use this as main photo");
            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if(currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await userRespository.SaveAllAsync()) return NoContent();
            return BadRequest("Problem setting main photo");
        }

        

    }
}