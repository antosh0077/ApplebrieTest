using ApplebrieTest.Common.ApiRequest;
using ApplebrieTest.Common.ApiResponces;
using ApplebrieTest.Common.DTOs;
using ApplebrieTest.Datas.Models;
using ApplebrieTest.Sercices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApplebrieTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SignInManager<AppUser> _signInManager;
        
        public UsersController(IUserService userService, SignInManager<AppUser> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }
       

        [HttpGet]
        [Route("GetAll")]
        public async Task<PagedResponse<List<UserDTO>>> GetAll([FromQuery] PagedRequest request)
        {
           //await _userService.SeedRandomUsers(500);
            var res = await _userService.GetAll(request);
            HttpContext.Session.SetString("Users", JsonConvert.SerializeObject(res));
            return res;
        }

        [HttpGet]
        [Route("GetAllFiltered")]
        public async Task<PagedResponse<List<UserDTO>>> GetAllFiltered([FromQuery] FilterRequest request)
        {
            //await _userService.SeedRandomUsers(500);
            var res = await _userService.GetAllFiltered(request);
            HttpContext.Session.SetString("Users", JsonConvert.SerializeObject(res));
            return res;
        }

        [HttpGet]
        [Route("GetAllWhichLoggedMoreThan2Times")]
        public async Task<PagedResponse<List<UserDTO>>> GetAllWhichLoggedMoreThan2Times([FromQuery] PagedRequest request)
        {
            var res = await _userService.GetAllWhichLoggedMoreThan2Times(request);

            return res;
        }

        
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Response<UserDTO>> Details(long? id)
        {
            return await _userService.GetByIdAsync(id);            
        }

        
        [HttpPost]
        [Route("Create")]
        public async Task<Response> Create(CreateUserDTO userDTO)
        {
            return await _userService.CreateAsync(userDTO);
        }

        
        [HttpPost]
        [Route("Edit")]
        public async Task<Response> Edit(UserUpdateDTO userDTO)
        {
            return await _userService.UpdateAsync(userDTO);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<Response> Delete(long id)
        {
            return await _userService.DeleteAsync(id);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<Response> Login(UserLoginDTO loginDTO)
        {
            var result =
                await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, loginDTO.RememberMe, false);
            if (!result.Succeeded)
            {
                return ApplebrieTest.Common.ApiResponces.Response.Failure(result.ToString());
            }
            else
            {
                var user = await _userService.GetByEmailAsync(loginDTO.Email);                
                return await _userService.UpdateLoginCountAsync(user);
            }
        }

        [HttpPost]
        [Route("Logout")]
        public async Task Logout()
        {            
            await _signInManager.SignOutAsync();            
        }

        [HttpGet]
        [Route("GetSessionData")]
        public async Task<PagedResponse<List<UserDTO>>> GetSessionData()
        {
            var users = HttpContext.Session.GetString("Users");

            return JsonConvert.DeserializeObject<PagedResponse<List<UserDTO>>>(users);
        }


        private bool AppUserExists(long id)
        {
            return true;
        }
    }
}
