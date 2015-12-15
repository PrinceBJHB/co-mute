using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class UpdateUserRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}