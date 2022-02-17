using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminModule.Models
{
    public class LoginModal
    {
        [Required(ErrorMessage ="This is Required")]
        [Display(Name ="Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "This is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}