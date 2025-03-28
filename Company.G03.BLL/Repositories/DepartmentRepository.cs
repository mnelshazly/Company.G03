﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;

namespace Company.G03.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext context) : base(context)
        {

        }

        //private readonly CompanyDbContext _context;

        //// Ask CLR to create object from CompanyDbContext
        //public DepartmentRepository(CompanyDbContext context)
        //{
        //    _context = context;
        //}

        //public IEnumerable<Department> GetAll()
        //{
        //    return _context.Departments.ToList();
        //}

        //public Department? Get(int id)
        //{
        //    return _context.Departments.Find(id);
        //}

        //public int Add(Department department)
        //{
        //    _context.Departments.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Update(Department department)
        //{
        //    _context.Departments.Update(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _context.Departments.Remove(department);
        //    return _context.SaveChanges();
        //}

    }
}
