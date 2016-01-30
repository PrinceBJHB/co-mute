using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class JoinCarPoolRequest
    {
        public string CarPoolId { get; set; }
        public string JoinerEmailAddress { get; set; }
    }
}