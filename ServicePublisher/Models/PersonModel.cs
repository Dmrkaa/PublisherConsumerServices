using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
namespace ServicePublisher.Models
{
    public class Person
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public Guid PersonID { get; set; }
    }
}
