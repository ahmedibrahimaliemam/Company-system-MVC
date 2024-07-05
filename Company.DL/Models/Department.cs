using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DL.Models
{
    public class Department
    {
        public int id {  get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        [Required]
        [MaxLength (50)]
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>() ;
    }
}
