using CaribeShop.Core.Application.SaveViewModel;
using CaribeShop.Core.Application.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaribeShop.Core.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<UserViewModel?> GetUserAsync(string username);
        Task<bool> Register(UserSaveViewModel model);

        Task<string?> Login(string username, string password);
        Task<string?> GetPasswordAsync(string username);

    }
}
