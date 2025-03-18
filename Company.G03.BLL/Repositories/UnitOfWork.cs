using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;

namespace Company.G03.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
