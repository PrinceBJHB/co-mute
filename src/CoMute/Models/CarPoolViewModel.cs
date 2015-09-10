using CoMute.Web.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    public class CarPoolViewModel
    {
        public CarPools CarPools { get; set; }

        public CarPoolDays CarPoolDays { get; set; }
    }
}