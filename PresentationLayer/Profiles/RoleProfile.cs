using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PresentationLayer.ViewModels;
using System.Collections;
using System.Collections.Generic;

namespace PresentationLayer.Profiles
{
    public class RoleProfile  :Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel> ().ForMember(D=> D.RoleName , O=>O.MapFrom(S=> S.Name)).ReverseMap(); 
        }
    }
}
