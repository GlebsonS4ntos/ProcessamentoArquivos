using AutoMapper;
using ProcessamentoArquivos.Domain.Entities;
using ProcessamentoArquivos.Service.Dtos;

namespace ProcessamentoArquivos.Service.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<ClienteDto, Cliente>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(c => c.Nome))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(c => c.Cpf))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateAt, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
