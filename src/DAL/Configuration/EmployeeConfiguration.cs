using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    /// <summary>
    /// Start configuration for employees.
    /// </summary>
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
            (
                new Employee
                {
                    EmployeeId = 1,
                    Age = 26,
                    Position = "Software developer",
                },
                new Employee
                {
                    EmployeeId = 2,
                    Age = 30,
                    Position = "Software developer",
                },
                new Employee
                {
                    EmployeeId = 3,
                    Age = 35,
                    Position = "Administrator",
                }
            );
        }
    }
}
