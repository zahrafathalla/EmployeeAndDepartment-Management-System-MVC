using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee> , IEmployeeRepository
    {

        private readonly ApplicationDbContext _context;
        public EmployeeRepository (ApplicationDbContext context) :base (context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEployeesByDepartment(string DepartmrntName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Search(string name)
        {
            var result = _context.Employees.Where(e
                => e.Name.Trim().ToLower().Contains(name.Trim().ToLower()) ||
                    e.Email.Trim().ToLower().Contains(name.Trim().ToLower()));
            return result;
        }

        //public int Add(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _context.Employees.Remove(employee);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //   return _context.Employees.ToList();
        //}

        //public Employee GetEmployeeById(int id)
        //{
        //   return _context.Employees.Find(id);
        //}

        //public int Update(Employee employee)
        //{
        //    _context.Employees.Update(employee);
        //    return _context.SaveChanges();
        //}
    }
}
