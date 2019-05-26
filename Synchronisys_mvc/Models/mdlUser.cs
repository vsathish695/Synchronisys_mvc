using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Synchronisys_mvc.Models
{
    public class mdlUser
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<UserDetails> data { get; set; }
    }

    public class UserDetails
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(50, ErrorMessage = "First name must be less than {1} characters")]
        public string first_name { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(50, ErrorMessage = "Last Name must be less than {1} characters")]
        public string last_name { get; set; }
        [Required(ErrorMessage = "Please enter email id")]
        [StringLength(50, ErrorMessage = "Email id must be less than {1} characters")]
        [RegularExpression(@"\S+@\S+\.\S+", ErrorMessage = "Please enter valid email id")]
        public string email { get; set; }
        public string avatar { get; set; }
    }

    public class StatusDesc
    {
        public int StatusCode { get; set; }
        public string Description { get; set; }
    }
}