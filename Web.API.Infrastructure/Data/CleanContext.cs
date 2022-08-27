using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.Entities;
using Web.API.Infrastructure.Data.Mappings;

namespace Web.API.Infrastructure.Data
{
    public class CleanContext:DbContext
    {
        public CleanContext(DbContextOptions<CleanContext> options)
            :base(options)
        {
           
        }

        public CleanContext()
            :base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Employee>(new EmployeeMapping());
        }

    }
}
