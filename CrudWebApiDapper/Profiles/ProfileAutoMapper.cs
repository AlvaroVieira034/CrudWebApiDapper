using AutoMapper;
using CrudWebApiDapper.Dto;
using CrudWebApiDapper.Models;

namespace CrudWebApiDapper.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<UsuarioModel, UsuarioListarDto>();
        }
    }
}
