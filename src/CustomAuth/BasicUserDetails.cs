using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth
{
    public class BasicUserDetails : IBasicUserDetails
    {
        [Required(ErrorMessage = "You need to type in your user name.")]
        [Display(Name = "Username", Prompt = "Username")]
        public string emailAddress { get; set; }

        [Required(ErrorMessage = "You need to type in your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
