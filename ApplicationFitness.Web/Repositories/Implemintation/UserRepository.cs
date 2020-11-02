using ApplicationFitness.Domain.Models;
using ApplicationFitness.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationFitness.Web.Repositories.Implemintation
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ApplicationFitnessWebContext context) : base(context)
        {

        }
    }
}
