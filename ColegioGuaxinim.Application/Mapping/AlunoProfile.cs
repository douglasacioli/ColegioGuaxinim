using AutoMapper;
using ColegioGuaxinim.Domain.Entities;
using static ColegioGuaxinim.Application.DTO.AlunoDTO;

namespace ColegioGuaxinim.Application.Mapping
{
    public class AlunoProfile : Profile
    {
        public AlunoProfile()
        {
            CreateMap<Aluno, AlunoListaDto>();
        }
    }
}
