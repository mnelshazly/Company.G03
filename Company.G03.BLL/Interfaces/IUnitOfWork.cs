using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository DepartmentRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }

        int Complete();
    }
}
