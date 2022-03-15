using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceConsumer.Models
{
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public uint OrganizationID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
