using Company.DL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Company.PL.Models
{
    public class DepartmentViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "The Name Is Required!")]
        [MaxLength(100)]
        public string name { get; set; }
        [Required(ErrorMessage = "The Code Is Required!")]
        [MaxLength(50)]
        public string Code { get; set; }
        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>();
    }
}
