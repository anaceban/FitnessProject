using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Identity.Register;

namespace WebApi.Services
{
    public interface IUserService
    {
        IList<IdentityUser> GetUsers();

        IdentityUser GetUserById(int id);

        IdentityUser AddNewUser(RegisterViewModel registerViewModel);

        IdentityUser UpdateUserDetails(int id, RegisterViewModel registerViewModel);

        IdentityUser UpdateUser(int id, RegisterViewModel registerViewModel);

        bool RemoveUserById(int id);
    }
}
