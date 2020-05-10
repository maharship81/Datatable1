using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreTutorial.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Department")]
        public Nullable<int> DepartmentId { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
        public virtual Department Department { get; set; }
        public virtual Sites Sites { get; set; }
        public bool IsDeleted { get; internal set; }
    }
}
