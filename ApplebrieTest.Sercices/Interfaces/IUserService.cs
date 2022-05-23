using ApplebrieTest.Common.ApiRequest;
using ApplebrieTest.Common.ApiResponces;
using ApplebrieTest.Common.DTOs;
using ApplebrieTest.Datas.Models;

namespace ApplebrieTest.Sercices.Interfaces
{
    public interface IUserService
    {
        bool Any();
        Task<Response> CreateAsync(CreateUserDTO userDTO);
        Task<Response> DeleteAsync(long? id);
        bool Exists(long? id);
        Task<PagedResponse<List<UserDTO>>> GetAll(PagedRequest request);
        Task<PagedResponse<List<UserDTO>>> GetAllWhichLoggedMoreThan2Times(PagedRequest request);
        Task<Response<UserDTO>> GetByIdAsync(long? id);
        Task<AppUser> GetByEmailAsync(string email);
        PagedRequest MakeInitialRequest(PagedRequest request);
        Task<Response> UpdateAsync(UserUpdateDTO updatedUser);
        Task<Response> UpdateLoginCountAsync(AppUser updatedUser);
        Task SeedRandomUsers(int count);
    }
}