using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceConsumer.Models
{
    public class Organization
    {
        [Key]
        [Required]
        public Guid OrganizationID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
