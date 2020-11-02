using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationFitness.Domain.Models
{
    public class ProgramType : BaseEntity
    {
        public string Name { get; set; }
        public List<ProgramSchedule> ProgramSchedules { get; set; }
    }
}
