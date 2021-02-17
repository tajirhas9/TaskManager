using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.ViewModels {
    public class RegisterViewModel {

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Password { get; set; }
     }
   
}
