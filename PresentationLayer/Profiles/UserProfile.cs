using AutoMapper;
using DataAccessLayer.Models;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();  
        }
    }
}
