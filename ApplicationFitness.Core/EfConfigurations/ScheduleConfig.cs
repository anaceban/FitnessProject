using ApplicationFitness.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFitness.Domain.Configurations
{
    public class ScheduleConfig : IEntityTypeConfiguration<ProgramSchedule>
    {
        public void Configure(EntityTypeBuilder<ProgramSchedule> builder)
        {

        }
    }
}
