using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Models
{
    
    public class Login
    {

        [Required]
        [EmailAddress]
        public string lemail { get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string lpassword { get; set;}

        
    
    }}