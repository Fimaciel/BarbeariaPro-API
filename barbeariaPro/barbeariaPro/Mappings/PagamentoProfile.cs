using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings;

public class PagamentoProfile : Profile
{
    public PagamentoProfile()
    {
        CreateMap<Pagamento, PagamentoDTO>();

        CreateMap<PagamentoDTO, Pagamento>();
    }
}
