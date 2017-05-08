﻿using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}