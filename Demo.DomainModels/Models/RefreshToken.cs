using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.DomainModels.Models
{
    public class RefreshToken
    {
        [Required]
        public string refresh_token { get; set; }
    }
}
