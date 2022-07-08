using DAM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAM_API.Models.Dtos;

namespace DAM_API.ProfilesIMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            // Source -> Target
            CreateMap<PratosDto, Prato>();
            CreateMap<Prato, PratosDto>();
            CreateMap<CantinaDto, Cantina>()
                .ForPath(dest => dest.Localizacao.Latitude, from => from.MapFrom(src => src.Latitude))
                .ForPath(dest => dest.Localizacao.Longitude, from => from.MapFrom(src => src.Longitude));
            CreateMap<Cantina, CantinaDto>()
               .ForPath(dest => dest.Latitude, from => from.MapFrom(src => src.Localizacao.Latitude))
               .ForPath(dest => dest.Longitude, from => from.MapFrom(src => src.Localizacao.Longitude));
            CreateMap<PratoDiaDto, PratoDia>();
            CreateMap<PratoDia, PratoDiaDto>();
            CreateMap<ReservaDto, Reserva>();
        }
    }
}
