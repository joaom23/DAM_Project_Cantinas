using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAM_BackOffice.Models;
using DAM_BackOffice.Models.Dtos;

namespace DAM_BackOffice.ProfilesIMapper
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
        }
    }
}
