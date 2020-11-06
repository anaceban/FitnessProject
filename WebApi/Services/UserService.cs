using ApplicationFitness;
using ApplicationFitness.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Identity.Register;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly FitnessAppContext _context;
        public UserService(FitnessAppContext context)
        {
            _context = context;
        }
        
        public User Create(UserDto user)
        {
            var _user = new User
            {
                Email = user.Email,
                PasswordHash = user.Password,
                UserName = user.Name,
                Weight = user.Weight,
                Height = user.Height,
                Gender = user.Gender,
                PrimaryGoal = user.PrimaryGoal,
                LevelOfFitnessExperience = user.LevelOfFitnessExperience,
                YearOfBirth = user.YearOfBirth
            };
            _context.Users.Add(_user);
            _context.SaveChanges();
            return _user;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return false; 
            }
            else _context.Users.Remove(user);
            _context.SaveChanges();
            return true;

        }

        public IList<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Update(UserDto user, int id)
        {
            var _user = _context.Users.Find(id);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            _user.UserName = user.Name;
            _user.Weight = user.Weight;
            _user.Height = user.Height;
            _user.Gender = user.Gender;
            _user.PrimaryGoal = user.PrimaryGoal;
            _user.LevelOfFitnessExperience = user.LevelOfFitnessExperience;
            _user.YearOfBirth = user.YearOfBirth;
            _context.SaveChanges();
            return _user;
        }
    }
}
