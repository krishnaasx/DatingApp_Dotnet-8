using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interface;
public interface IUserRespository {
        
    void Update(AppUser user);
    Task<IEnumerable<AppUser>> GetUserAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
    Task<MemberDto?> GetMemberAsync(string username);
}