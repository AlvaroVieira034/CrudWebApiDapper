using CrudWebApiDapper.Dto;
using CrudWebApiDapper.Models;

namespace CrudWebApiDapper.Services
{
    public interface IUsuarioInterface
    {
        
        Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios();
        Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int idUsuario);
        Task<ResponseModel<List<UsuarioListarDto>>> InserirUsuario(UsuarioCriarDto usuarioCriarDto);
        Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);
        Task<ResponseModel<List<UsuarioListarDto>>> ExcluirUsuario(int idUsuario);
    }
}
