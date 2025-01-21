using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auth.Commands;
using AutoMapper;
using BCrypt.Net;
using Domain.Models;

namespace Application.Features.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterRequest, User>().ForMember(u => u.PasswordHash,
                option => option.MapFrom(request => request.Password
            ));
        }
    }
}
