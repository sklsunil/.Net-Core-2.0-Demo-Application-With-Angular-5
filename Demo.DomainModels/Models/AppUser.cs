using Demo.DomainModels.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.DomainModels.Models
{
    public class AppUser
    {
        [Required]
        public string Username { get; set; }


        [Required(ErrorMessage = "The email address is required")]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "Must be between 5 and 16 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string DisplayName { get; set; }

        // Roles
        // Admin= 1, User= 2 , Editor= 3
        public CustomRoles Role { get; set; }

    }
}
