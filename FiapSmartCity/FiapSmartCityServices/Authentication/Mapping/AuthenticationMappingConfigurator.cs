using AutoMapper;
using FiapSmartCityDomain.Authentication;
using FiapSmartCityDomain.Authentication.ViewModel;
using FiapSmartCityDomain.Entities;
using FiapSmartCityDomain.TrafficManagement.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace FiapSmartCityServices.Authentication.Mapping
{
    public class AuthenticationMappingConfigurator : Profile
    {
        public AuthenticationMappingConfigurator(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, LoginViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => GetDisplayName(src)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();


            cfg.CreateMap<User, UserViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FullName));

            cfg.CreateMap<IdentityRole, RoleViewModel>()
                .ReverseMap();


            cfg.CreateMap<AccidentsEntity, AccidentsViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.AddressComplement, opt => opt.MapFrom(src => src.Address.Complement))
                .ForMember(dest => dest.AddressDescription, opt => opt.MapFrom(src => src.Address.Description))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Cars.Model))
                .ForMember(dest => dest.CarBrand, opt => opt.MapFrom(src => src.Cars.Brand))
                .ForMember(dest => dest.CarYear, opt => opt.MapFrom(src => src.Cars.Year));
        }


        private static string GetDisplayName(User user)
        {
            var result = user.DisplayName;
            if (!string.IsNullOrWhiteSpace(user?.FullName))
            {
                var fullNameParts = user.FullName.Split(' ');
                if (fullNameParts?.Length > 0)
                {
                    result = $"{fullNameParts.FirstOrDefault()?.ToUpper().FirstOrDefault()}";
                    if (fullNameParts.Length > 1)
                    {
                        result += $"{fullNameParts.LastOrDefault()?.ToUpper().FirstOrDefault()}";
                    }
                }
            }
            return result;
        }
    }
}
