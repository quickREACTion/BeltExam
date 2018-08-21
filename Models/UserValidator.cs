using System;
using System.ComponentModel.DataAnnotations;


namespace BeltExam
{
    public class UserValidator
    {   
        [Required(ErrorMessage= "First Name is a required field!")]
        [MinLength(2)]
        [DataType(DataType.Text)]
        public string First_Name { get; set; }

        [Required(ErrorMessage= "Last Name is a required field!")]
        [MinLength(2)]
        [DataType(DataType.Text)]
        public string Last_Name { get; set; }

        [Required(ErrorMessage= "Email is a required field!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage= "Password is a required field!")]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d][A-Za-z\d!@#$%^&*()_+]{7,19}$", ErrorMessage="The password must contain 1 letter, 1 number, 1 special character!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage="Password is a required field!")]
        [DataType(DataType.Password)]
        [CompareAttribute("Password")]
        public string Confirm {get; set;}

    }



}