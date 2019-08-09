using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cali_Remote_Pc.Models
{
    public class LoginViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name ="Username")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
