using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Identity;

namespace WebApi.Services
{
    public interface IUserService
    {

        User GetUserById(int id);

        User AddNewUser(RegisterModelDto dto);


        void RemoveUserById(int id);
        User UpdateUserProfile(UserProfileDto dto, User user);
        List<User> GetUsers();
    }
}
