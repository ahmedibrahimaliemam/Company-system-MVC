using Company.DL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Company.PL.Models
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "the chars must be less than 50")]

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
        public string ImageName { get; set; }
        public IFormFile Image {  get; set; }

        public string phone { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        //[InverseProperty(nameof(DepartmentId))]
        public Department Department { get; set; }
    }
}
