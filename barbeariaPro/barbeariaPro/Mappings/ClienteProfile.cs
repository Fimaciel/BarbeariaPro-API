using AutoMapper;
using barbeariaPro.Models;
using barbeariaPro.DTOs;

namespace barbeariaPro.Mappings
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
        }
    }
}
