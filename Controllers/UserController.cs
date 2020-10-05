using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vedma_backend.Entity;
using Vedma_backend.Models;
using Vedma_backend.Services.Contracts;
using Vedma_backend.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vedma_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(UserLoginVm loginVm)
        {
            return await _userService.Login(loginVm);
        }


        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserRegisterVm registerVm)
        {
            return await _userService.Register(registerVm);
        }
    }
}
