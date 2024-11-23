using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(50) ]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }


        [InverseProperty("department")]
        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>(); 
    }
}
