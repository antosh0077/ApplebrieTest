using ApplebrieTest.Common.ApiRequest;
using ApplebrieTest.Common.ApiResponces;
using ApplebrieTest.Datas.Data;
using ApplebrieTest.Common.DTOs;
using ApplebrieTest.Datas.Models;
using ApplebrieTest.Sercices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplebrieTest.Sercices.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<PagedResponse<List<UserDTO>>> GetAll(PagedRequest request)
        {
            var users = await _context.Users
                .Select(u => new UserDTO(u))
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return PagedResponse<List<UserDTO>>.CreatePagedReponse<UserDTO>(users, request, _context.Users.Count());
        }

        public async Task<PagedResponse<List<UserDTO>>> GetAllFiltered(FilterRequest request)
        {
            var users = _context.Users
                .Where(u => u.UserType == request.FilterByUserType);

            var pagedData = await users .Select(u => new UserDTO(u))
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return PagedResponse<List<UserDTO>>.CreatePagedReponse<UserDTO>(pagedData, request, users.Count());
        }

        public async Task<PagedResponse<List<UserDTO>>> GetAllWhichLoggedMoreThan2Times(PagedRequest request)
        {
            var users = _context.Users
                .Where(u => u.LoginCount > 2);
            var pagedData = await users.Select(u => new UserDTO(u))
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return PagedResponse<List<UserDTO>>.CreatePagedReponse<UserDTO>(pagedData, request, users.Count());
        }

        public PagedRequest MakeInitialRequest(PagedRequest request)
        {
            request.PageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize > 10 ? 10 : request.PageSize;
            return request;
        }

        public static bool RequestIsValid(PagedRequest request)
        {
            return request.PageNumber > 1 && request.PageSize > 0;
        }

        public bool Any()
        {
            return _context.Users.Any();
        }

        public async Task<Response> CreateAsync(CreateUserDTO userDTO)
        {
            AppUser user = new AppUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserType = userDTO.UserType
            };

            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (!result.Succeeded)
            {
                var errors = new List<Error>();
                foreach (var error in result.Errors)
                {
                    errors.Add(new Error(error.Description));
                }
                return Response.Failure(errors);
            }

            return Response.Success();
        }

        public async Task<Response> DeleteAsync(long? id)
        {
            AppUser user = await _context.Users?.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _context.Remove(user);
                var result = await _context.SaveChangesAsync();
                if (result < 1)
                {
                    return Response.Failure("User not deleted");
                }
                return Response.Success();
            }
            return Response.Failure("User not found");
        }

        public bool Exists(long? id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();

        }

        public async Task<Response<UserDTO>> GetByIdAsync(long? id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            return new Response<UserDTO>(new UserDTO(user));
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<Response> UpdateAsync(UserUpdateDTO updatedUser)
        {
            AppUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

            if (user != null)
            {
                user.UserType = updatedUser.UserType;
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Email = updatedUser.Email;
                user.UserName = updatedUser.Email;

                _context.Update(user);
                var result = await _context.SaveChangesAsync();
                if (result < 1)
                {
                    return Response.Failure("User not updated");
                }
                return Response.Success();
            }
            return Response.Failure("User not found");

        }

        public async Task<Response> UpdateLoginCountAsync(AppUser user)
        {
            if (user != null)
            {
                user.LoginCount++;
                _context.Update(user);
                var result = await _context.SaveChangesAsync();
                if (result < 1)
                {
                    return Response.Failure("User login count not updated");
                }
                return Response.Success();
            }
            return Response.Failure("User not found");
        }

        public async Task SeedRandomUsers(int count)
        {
            for (int i = 0; i < 500; i++)
            {

                var user = new CreateUserDTO()
                {
                    FirstName = "Test" + i,
                    LastName = "Testyan" + i,
                    Email = "Testyan" + i + "@gmail.com",
                    UserType = UserType.User,
                    Password = "Pass123#",
                    ConfirmPassword = "Pass123#"
                };
                if (i % 100 == 0)
                {
                    user.UserType = UserType.Admin;
                }

                await CreateAsync(user);
            }
        }


    }
}
