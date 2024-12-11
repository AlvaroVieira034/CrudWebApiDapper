using AutoMapper;
using CrudWebApiDapper.Dto;
using CrudWebApiDapper.Models;
using Dapper;
using System.Data.SqlClient;

namespace CrudWebApiDapper.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;   
        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioListarDto>> resposta = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection.QueryAsync<UsuarioModel>("select * from Usuarios");

                if (usuariosDB.Count() == 0) 
                {
                    resposta.Mensagem = "Nenhum usuário localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                // Transformação Mapper
                var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(usuariosDB);

                resposta.Dados = usuarioMap;
                resposta.Mensagem = "Usuários localizados com sucesso!";
                resposta.Status = true;
                return resposta;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int idUsuario)
        {
            ResponseModel<UsuarioModel> resposta = new ResponseModel<UsuarioModel>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection.QueryFirstOrDefaultAsync<UsuarioModel>("select * from Usuarios where id = @id", new {Id = idUsuario});

                if (usuariosDB == null)
                {
                    resposta.Mensagem = "Nenhum usuário localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                // Transformação Mapper
                //var usuarioMap = _mapper.Map<UsuarioModel>(usuariosDB);

                resposta.Dados = usuariosDB;
                resposta.Mensagem = "Usuário localizado com sucesso!";
                resposta.Status = true;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> InserirUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseModel<List<UsuarioListarDto>> resposta = new ResponseModel<List<UsuarioListarDto>>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection
                    .ExecuteAsync("insert into Usuarios(NomeCompleto, Email, Cargo, Salario, CPF, Situacao, Senha) " +
                    "values (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Situacao, @Senha)", usuarioCriarDto);

                if (usuariosDB == 0)
                {
                    resposta.Mensagem = "Nenhum usuário localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                var usuarios = await ListarUsuarios(connection);

                // Transformação Mapper
                var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                resposta.Dados = usuarioMap;
                resposta.Mensagem = "Usuário inserido com sucesso!";
                resposta.Status = true;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseModel<List<UsuarioListarDto>> resposta = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection
                    .ExecuteAsync("update Usuarios set NomeCompleto = @NomeCompleto, " +
                                                      "Email = @Email, " +
                                                      "Cargo = @Cargo, " +
                                                      "Salario = @Salario, " +
                                                      "CPF = @CPF, " +
                                                      "Situacao = @Situacao where Id = @Id", usuarioEditarDto);


                if (usuariosDB == 0)
                {
                    resposta.Mensagem = "Ocorreu um erro ao editar o registro!";
                    resposta.Status = false;
                    return resposta;
                }

                var usuarios = await ListarUsuarios(connection);

                // Transformação Mapper
                var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                resposta.Dados = usuarioMap;
                resposta.Mensagem = "Usuário editado com sucesso!";
                resposta.Status = true;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> ExcluirUsuario(int idUsuario)
        {
            ResponseModel<List<UsuarioListarDto>> resposta = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection.ExecuteAsync("delete from Usuarios where id = @id", new { Id = idUsuario });

                if (usuariosDB == 0)
                {
                    resposta.Mensagem = "Nenhum usuário localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                var usuarios = await ListarUsuarios(connection);

                // Transformação Mapper
                var usuariosMap = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                resposta.Dados = usuariosMap;
                resposta.Mensagem = "Usuário excluido com sucesso!";
                resposta.Status = true;
                return resposta;
            }
        }

        private static async Task<IEnumerable<UsuarioModel>> ListarUsuarios(SqlConnection _connection)
        {
            return await _connection.QueryAsync<UsuarioModel>("select * from Usuarios");
        }

        
    }
}
