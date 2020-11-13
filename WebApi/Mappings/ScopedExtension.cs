using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Mappings
{
    public static class ScopedExtension
    {
        public static void AddScoped(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProgramScheduleService, ProgramScheduleService>();
            services.AddScoped<IProgramTypeService, ProgramTypeService>();
            services.AddScoped<IUserReviewService, UserReviewService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IUserScheduleService, UserScheduleService>();
        }
    }
}
