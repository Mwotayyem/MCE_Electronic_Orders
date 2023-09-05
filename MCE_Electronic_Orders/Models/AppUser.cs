using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace MCE_Electronic_Orders.Models
{
    public class AppUser: IdentityUser
    {
        

        public string PHONENUM { get; set; }

        public string BRANCHID { get; set; }

        public int USERTYPE { get; set; }

      
    }
}
