using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DL.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="the chars must be less than 50")]
        
        public string name { get; set; }
        [Required]
        
        public string email { get; set; }
        [Required]
        
        public int age { get; set; }
        [Required]
        
        public string address { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal salary { get; set; }
        [Required]
        public DateTime hirringDate { get; set; }
        bool isActive { get; set; }
       
        public string phone { get; set; }

        public DateTime dateOfCreation = DateTime.Now;
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public string ImageName { get; set; }
       //[InverseProperty(nameof(DepartmentId))]
        public Department Department { get; set; }

    }
}
