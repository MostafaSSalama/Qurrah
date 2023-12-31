﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Entities
{
    public class UserType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public UserTypeId Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}