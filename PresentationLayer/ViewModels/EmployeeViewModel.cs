using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace PresentationLayer.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length Is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length Is 5 Chars")]
        public string Name { get; set; }



        [Range(20, 35, ErrorMessage = "Age Must Be In Range From 20 To 35")]
        public int? Age { get; set; }



        //[RegularExpression(@"^\d+-[A-Za-z0-9]+-[A-Za-z0-9]+-[A-Za-z0-9]+$",
        //    ErrorMessage = "Address Must Be Like 123-street-city-country ")]
        public string Address { get; set; }



        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }



        public bool IsActive { get; set; }



        [EmailAddress]
        public string Email { get; set; }



        [Phone]
        public string PhoneNumber { get; set; }


        public DateTime HireDate { get; set; }


        public IFormFile Image { get; set; }
        public string ImageName { get; set; }


        public DateTime DateOfCreation { get; set; }


        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }


        [InverseProperty("employees")]
        public Department department { get; set; }
    }
}
