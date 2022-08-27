using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.Entities;

namespace Web.API.Infrastructure.Data.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmpId);

            builder.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DateOfBirth).HasColumnType("date");

            builder.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.EmpName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Gender).HasMaxLength(10);
        }
    }
}
