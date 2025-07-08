using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings;

public class AgendamentoProfile : Profile
{
    public AgendamentoProfile()
    {
        CreateMap<Agendamento, AgendamentoDTO>();

        CreateMap<AgendamentoDTO, Agendamento>();
    }
}
