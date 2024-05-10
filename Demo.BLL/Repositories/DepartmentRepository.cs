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
    public class DepartmentRepository :GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Department> Search(string name)
        {
            var result = _context.Department.Where(e
              => e.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
            return result;

           
        }
        //public int Add(Department department)
        //{
        //    _context.Department.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _context.Department.Remove(department);
        //    return _context.SaveChanges(); ;
        //}

        //public IEnumerable<Department> GetAll()
        //{
        //    return _context.Department.ToList();
        //}

        //public Department GetDepartmentById(int? id)
        //{
        //    return _context.Department.Find(id);
        //}

        //public int Update(Department department)
        //{
        //    _context.Department.Update(department);
        //    return _context.SaveChanges();
        //}

    }
}
