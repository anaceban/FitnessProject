using ApplicationFitness.Domain.Models;
using ApplicationFitness.Web.Areas.Identity.Data;
using ApplicationFitness.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationFitness.Web.Repositories.Implemintation
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
