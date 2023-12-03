using Employee.Messages.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Repository.Entity
{
    [Table("Employees", Schema = "Employee")]
    public class EmployeeEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public EmployeeRole Role { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegDate { get; set; }

        public EmployeeEntity()
        {

        }
    }
}
