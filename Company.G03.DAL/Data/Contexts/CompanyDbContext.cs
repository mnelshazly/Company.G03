﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.G03.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.DAL.Data.Contexts
{
    public class CompanyDbContext : DbContext
    {
        // CLR 
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = CompanyG03; Trusted_Connection = true; TrustServerCertificate = true ");
        //}

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
