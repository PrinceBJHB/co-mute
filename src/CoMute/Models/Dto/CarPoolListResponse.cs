using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoMute.Web.Models.Dto
{
    public class CarPoolListResponse
    {
        public List<CarPool> data { get; set; }

        public int total { get; set; }

        public int page { get; set; }

        public int records { get; set; }
    }
}
