using Application.Features.Auth.Commands;
using Application.Interfaces;
using Application.Query;
using AutoMapper;
using BCrypt.Net;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IUserRoleRepository _userRole;
    private readonly ITokenService _token;
    private readonly IMapper _mapper;
    public AuthService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, ITokenService token, IMapper mapper)
    {
        _userRepo = userRepository;
        _userRole = userRoleRepository;
        _token = token;
        _mapper = mapper;
    }
    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var userObj = await _userRepo.GetUserByEmailAsync(UserQuery.GetByEmail, request.Email);
        if (userObj is not null)
        {
            var IsAuthenticated = Authenticated(request.Password, userObj.PasswordHash);
            if (!IsAuthenticated) return new AuthResult()
            {
                Success = false,
                Message = "Failed to login"
            };
            return new AuthResult()
            {
                Success = true,
                Message = "Login successfully",
                Token = _token.GenerateToken(userObj,"customer")
            };
        }
        return new AuthResult()
        {
            Success = false,
            Message = "Failed to login"
        };
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var userObj = _mapper.Map<User>(request);
        userObj.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var result = await _userRepo.AddAsync(UserQuery.AddUser, userObj);
        if (result is null)
        {
            return new AuthResult()
            {
                Success = false,
                Message = "Failed to register"
            };
        }
        return new AuthResult()
        {
            Success = true,
            Message = "Register successfully"
        };

    }
    private bool Authenticated(string password, string hashpassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashpassword);
    }
}