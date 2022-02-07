using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceConsumer.Models
{
    public class User
    {
        [Required]
        [Key]
        public Guid UserID { get; set; }
        [ForeignKey("OrganizationInfoKey")]
        public Guid? OrganizationID { get; set; } //внешний ключ

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        public string Email { get; set; }


    }
}
