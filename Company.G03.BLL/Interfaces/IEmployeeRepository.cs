using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G03.DAL.Models;

namespace Company.G03.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<Employee>> GetByNameAsync(string name);

        //IEnumerable<Employee> GetAll();

        //Employee? Get(int id);

        //int Add(Employee employee);

        //int Update(Employee employee);

        //int Delete(Employee employee);
    }
}
