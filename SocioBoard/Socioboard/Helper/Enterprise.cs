using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Socioboard.Helper
{
    public class Enterprise
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string CompanyWebsite { get; set; }
        [Required]
        public string ContactEmailId { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }
       
    }
}