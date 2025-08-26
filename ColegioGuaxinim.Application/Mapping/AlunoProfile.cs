using AutoMapper;
using ColegioGuaxinim.Domain.Entities;
using static ColegioGuaxinim.Application.DTO.AlunoDTO;

namespace ColegioGuaxinim.Application.Mapping
{
    public class AlunoProfile : Profile
    {
        public AlunoProfile()
        {
            CreateMap<Aluno, AlunoListaDto>()
              .ConstructUsing(a => new AlunoListaDto(a.Id, a.Nome, a.Mensalidade, a.DataDeVencimento));
        }
    }
}
