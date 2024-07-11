using AutoMapper;
using Tamagotchi.Data;

namespace Tamagotchi.Mapping
{
    public class PetsProfile : Profile
    {
        public PetsProfile() {
            CreateMap<Pets, CurrentTamagotchi>()
                .ForMember(dest => dest.Age_State, opt => opt.MapFrom(src => "false"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Id_User, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Health, opt => opt.MapFrom(src => src.Statistics.Health))
                .ForMember(dest => dest.Hunger, opt => opt.MapFrom(src => src.Statistics.Hunger))
                .ForMember(dest => dest.Energy, opt => opt.MapFrom(src => src.Statistics.Energy))
                .ForMember(dest => dest.Hygiene, opt => opt.MapFrom(src => src.Statistics.Hygiene))
                .ForMember(dest => dest.Fun, opt => opt.MapFrom(src => src.Statistics.Fun))
                .ForMember(dest => dest.Max_Health, opt => opt.MapFrom(src => src.Statistics.Max_Health))
                .ForMember(dest => dest.Max_Hunger, opt => opt.MapFrom(src => src.Statistics.Max_Hunger))
                .ForMember(dest => dest.Max_Energy, opt => opt.MapFrom(src => src.Statistics.Max_Energy))
                .ForMember(dest => dest.Max_Hygiene, opt => opt.MapFrom(src => src.Statistics.Max_Hygiene))
                .ForMember(dest => dest.Max_Fun, opt => opt.MapFrom(src => src.Statistics.Max_Fun));

        }
    }
}
