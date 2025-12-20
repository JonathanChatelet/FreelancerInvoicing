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
            CreateMap<User, ReadMyInfoDTO>();
            CreateMap<User, ReadUserDTO>();
            CreateMap<CreateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
            CreateMap<UpdateMyInfoDTO, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
            CreateMap<UpdateUserDTO, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

