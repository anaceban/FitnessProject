using ApplicationFitness.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Identity.Register;

namespace WebApi.Services
{
    public interface IUserService
    {
        IList<User> GetAll();
        User GetById(int id);
        User Create(UserDto user);
        User Update(UserDto user, int id);
        bool Delete(int id);
    }
}
