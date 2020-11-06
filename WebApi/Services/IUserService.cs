using ApplicationFitness.Domain.Models;
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
        User Authenticate(string username, string password);
        IList<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}
