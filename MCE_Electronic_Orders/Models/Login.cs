using System.ComponentModel.DataAnnotations;

namespace MCE_Electronic_Orders.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        [Required]
        public bool RememberMe { get; set; }


    }
}
