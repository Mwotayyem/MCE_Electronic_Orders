using System.ComponentModel.DataAnnotations;

namespace MCE_Electronic_Orders.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
