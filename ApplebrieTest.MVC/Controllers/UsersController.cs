using ApplebrieTest.Datas.ApiRequest;
using ApplebrieTest.Datas.ApiResponces;
using ApplebrieTest.Datas.Data;
using ApplebrieTest.Datas.DTOs;
using ApplebrieTest.Datas.Models;
using ApplebrieTest.Sercices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ApplebrieTest.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;

        public UsersController(AppDbContext context, IUserService userService, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userService = userService;
            _signInManager = signInManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'ApplebrieTestMVCContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var userTypes = Enum.GetValues(typeof(UserType)).Cast<UserType>().Skip(1).ToList();
            ViewData["UserTypes"] = new SelectList(userTypes);
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,UserType,Email,Password,ConfirmPassword")] CreateUserDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(user);                
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userTypes = Enum.GetValues(typeof(UserType)).Cast<UserType>().Skip(1).ToList();
            ViewData["UserTypes"] = new SelectList(userTypes);
            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(new UserUpdateDTO()
            {
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                UserName = appUser.UserName,
                UserType = appUser.UserType
            });
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FirstName,LastName,UserType,Id,UserName,Email")] UserUpdateDTO appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var res = await _userService.UpdateAsync(appUser);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(new UserDTO()
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                LoginCount = appUser.LoginCount,
                UserType = appUser.UserType
            });
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplebrieTestMVCContext.Users'  is null.");
            }
            var res = await _userService.DeleteAsync(id);

            
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
