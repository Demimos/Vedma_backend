using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vedma_backend.Entity;
using Vedma_backend.Infrastructure.Contracts;
using Vedma_backend.Models;
using Vedma_backend.Services.Contracts;
using Vedma_backend.ViewModel;

namespace Vedma_backend.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ApplicationContext _context;

        public UserService(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            IJwtGenerator jwtGenerator,
            ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _context = context;
        }
        public async Task<User> Login(UserLoginVm loginVm)
        {
            try
            {
                new MailAddress(loginVm.Email);
            }
            catch
            {
                throw new ArgumentException("Value has a invalid format.", nameof(loginVm.Email));
            }

            if (string.IsNullOrWhiteSpace(loginVm.Password))
            {
                throw new ArgumentException("Value can't be null or empty.", nameof(loginVm.Password));
            }

            var user = await _userManager.FindByEmailAsync(loginVm.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User doesn't exist.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVm.Password, false);

            if (result.Succeeded)
            {
                return new User()
                {
                    Email = user.Email,
                    Username = user.UserName,
                    Token = _jwtGenerator.CreateToken(user)
                };
            }

            throw new UnauthorizedAccessException("Password is invalid.");
        }

        public async Task<User> Register(UserRegisterVm userRegisterVm)
        {
            try
            {
                new MailAddress(userRegisterVm.Email);
            }
            catch
            {
                throw new ArgumentException("Value has a invalid format.", nameof(userRegisterVm.Email));
            }

            if (string.IsNullOrWhiteSpace(userRegisterVm.Password))
            {
                throw new ArgumentException("Value can't be null or empty.", nameof(userRegisterVm.Password));
            }

            if (await _context.Users.AnyAsync(x => x.Email == userRegisterVm.Email))
            {
                throw new Exception("The email provided is already registered.");
            }

            var user = new AppUser()
            {
                Email = userRegisterVm.Email,
                UserName = userRegisterVm.Username
            };

            var result = await _userManager.CreateAsync(user, userRegisterVm.Password);

            if (result.Succeeded)
            {
                return new User()
                {
                    Email = user.Email,
                    Username = user.UserName,
                    Token = _jwtGenerator.CreateToken(user)
                };
            }

            var errors = new StringBuilder();

            foreach (var error in result.Errors)
            {
                errors.Append($"{error.Description} ");
            }

            throw new Exception($"Failed to create user due to: \n{errors}");
        }
    }
}
