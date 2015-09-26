using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models.Dto
{
    public class CarPoolListRequest
    {
        [Required]
        public int page { get; set; }

        [Required]
        public int rows { get; set; }

        [Required]
        public string sord { get; set; }

        [Required]
        public string sidx { get; set; }
        public string search { get; set; }
    }
}
