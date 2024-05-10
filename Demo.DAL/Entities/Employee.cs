using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Employee: BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Employee Name Is Required")]
        public string Name { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [Column (TypeName ="Money")]
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public string ImageUrl { get; set; }


    }
}
