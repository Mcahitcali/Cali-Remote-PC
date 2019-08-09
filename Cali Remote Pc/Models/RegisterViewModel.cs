using System.ComponentModel.DataAnnotations;

namespace Cali_Remote_Pc.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(256)]
        public string LastName { get; set; }

        [Required, MaxLength(256)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


    }
}