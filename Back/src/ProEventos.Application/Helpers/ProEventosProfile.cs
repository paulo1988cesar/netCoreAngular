using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain;

namespace ProEventos.Application.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<LoteDto, Lote>().ReverseMap();
            CreateMap<RedeSocialDto, RedeSocial>().ReverseMap();
            CreateMap<PalestranteDto, PalestranteDto>().ReverseMap();
        }
    }
}