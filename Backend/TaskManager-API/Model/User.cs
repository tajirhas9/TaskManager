using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Model {
    public class User : IEntityBase {

        [Key]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
