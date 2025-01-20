﻿namespace Application.Features.Auth.Commands;

public class AuthResult
{
    public bool Success { get; set; }
    public string Token { get; set; }
    public string Message { get; set; }
}