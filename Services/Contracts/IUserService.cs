using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma_backend.Entity;
using Vedma_backend.Models;
using Vedma_backend.ViewModel;

namespace Vedma_backend.Services.Contracts
{
    public interface IUserService
    {
        Task<User> Login(UserLoginVm loginVm);
        Task<User> Register(UserRegisterVm userRegisterVm);
    }
}
