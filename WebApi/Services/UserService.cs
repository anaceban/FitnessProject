using ApplicationFitness;
using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Identity;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly FitnessAppContext _context;
        public UserService(FitnessAppContext context)
        {
            _context = context;    
        }
        public User AddNewUser(RegisterModelDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public void RemoveUserById(int id)
        {
            var user = _context.Users.Find(id);
            _context.Remove(user);
            _context.SaveChanges();
        }

        public User UpdateUserProfile(UserProfileDto dto, User user)
        {
            if (user != null)
            {
                user.Gender = dto.Gender;
                user.YearOfBirth = dto.YearOfBirth;
                user.Weight = dto.Weight;
                user.Height = dto.Height;
                user.PrimaryGoal = dto.PrimaryGoal;
                user.LevelOfFitnessExperience = dto.LevelOfFitnessExperience;
                _context.SaveChanges();
                return user;
            }
            else return null;
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
