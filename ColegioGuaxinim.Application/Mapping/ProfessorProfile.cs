using AutoMapper;
using ColegioGuaxinim.Domain.Entities;
using static ColegioGuaxinim.Application.DTO.ProfessorDTO;

namespace ColegioGuaxinim.Application.Mapping
{
    public class ProfessorProfile : Profile
    {
        public ProfessorProfile()
        {
            CreateMap<Professor, ProfessorListaDto>();
            CreateMap<Professor, ProfessorDetalheDto>()
                .ForMember(d => d.QuantidadeAlunos,
                           opt => opt.MapFrom(s => s.Alunos != null ? s.Alunos.Count : 0));

            CreateMap<ProfessorCriarDto, Professor>();

            CreateMap<ProfessorEditarDto, Professor>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Alunos, opt => opt.Ignore())
                .ForMember(d => d.BloquearTempoDeImportacao, opt => opt.Ignore());
        }
    }
}
