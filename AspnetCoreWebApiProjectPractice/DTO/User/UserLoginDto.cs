﻿using System.ComponentModel.DataAnnotations;

namespace AspnetCoreWebApiProjectPractice.DTO.User
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
