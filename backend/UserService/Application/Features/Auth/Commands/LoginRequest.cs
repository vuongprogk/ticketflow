﻿using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.Commands;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}