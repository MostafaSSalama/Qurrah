﻿using System.ComponentModel.DataAnnotations;

namespace Qurrah.Entities
{
    public class LoginRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}