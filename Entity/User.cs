using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The username is mandatory")]
        [StringLength(50, ErrorMessage = "Username needs to be shorter than 50 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The name is mandatory")]
        [StringLength(50, ErrorMessage = "Name needs to be shorter than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The last name is mandatory")]
        [StringLength(50, ErrorMessage = "Last name needs to be shorter than 50 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The email is mandatory")]
        [EmailAddress(ErrorMessage = "Email adress is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The title is mandatory")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The password is mandatory")]
        [MinLength(6, ErrorMessage = "Password needs to have more than 6 characters")]
        public string Password { get; set; }
        public string? Photo { get; set; }

    }
}
