﻿using System.ComponentModel.DataAnnotations;

namespace ToDo.App.Api.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}