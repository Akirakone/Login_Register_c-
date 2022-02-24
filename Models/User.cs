using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Models
{
    
    public class User
    {
        [Key]
        public int userid { get; set;}

        [Required]
        [MinLength(2)]
        public string firstname { get; set;}

        [Required]
        [MinLength(2)]
        public string lastname { get; set;}

        [Required]
        [EmailAddress]
        public string email { get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set;}


        [NotMapped]
        [Compare("password")]
        [DataType(DataType.Password)]
        public string confirm { get; set;}
        
    
    }}