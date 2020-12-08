using ApplicationFitness;
using ApplicationFitness.Domain.Models;
using ApplicationFitness.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Identity;
using WebApi.Sorting;
using System.Reflection;

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
                UserName = dto.Email,
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
                user.NumberOfCaloriesPerDay = GetNumberOfCaloriesPerDay(user);
                _context.SaveChanges();
                return user;
            }
            else return null;
        }

        public IEnumerable<User> GetUsersFiltered(SampleFilterModel filter)
        {
            var properyInfo = typeof(User);
            var propery = properyInfo.GetProperty(filter.SortedField ?? "FirstName");
            if (string.IsNullOrEmpty(filter.Term))
            {
                var allUsers = GetUsers() as IEnumerable<User>;
                allUsers = filter.SortAsc ? allUsers.OrderBy(p => propery.GetValue(p)) : allUsers.OrderByDescending(p => propery.GetValue(p));
                return allUsers;
            }
            var users = _context.Users.Where(u => u.FirstName.StartsWith(filter.Term) || u.LastName.StartsWith(filter.Term)).AsEnumerable();
            users = filter.SortAsc ? users.OrderBy(p => propery.GetValue(p)) : users.OrderByDescending(p => propery.GetValue(p));
            return users.ToList();
        }
        
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public int GetNumberOfCaloriesPerDay(User user)
        {
            var numberOfCal = 0;
            if (user.Gender == "male")
            {
                numberOfCal = 88 + (13 * user.Weight) + (5 * user.Height) - (6 * (DateTime.Now.Year - user.YearOfBirth));
            }
            else numberOfCal = 450 + (9 * user.Weight) + (3 * user.Height) - (4 * (DateTime.Now.Year - user.YearOfBirth));
            return numberOfCal;

        }
    }
}
