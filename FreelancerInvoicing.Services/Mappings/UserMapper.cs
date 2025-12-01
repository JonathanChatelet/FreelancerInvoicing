using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.DTO.Users;

namespace FreelancerInvoicing.Services.Mappings
{
    public class CreateUserProfile : Profile
    {
        public CreateUserProfile()
        {
            CreateMap<User, ReadUserDTO>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDTO, User>();
        }
    }
}

