using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace TaskManagerApps.Models
{
    public class Users
    {
        [Key]
        [Required(ErrorMessage = "Please enter your User ID.")]
        [Display(Name = "Username : ")]
        public int UserId { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your Password.")]
        [Display(Name = "Password : ")]
        public string PasswordHash { get; set; }
    }
}
