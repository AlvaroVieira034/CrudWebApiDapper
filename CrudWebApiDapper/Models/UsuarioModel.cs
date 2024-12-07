namespace CrudWebApiDapper.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public string CPF { get; set; }
        public bool Situacao { get; set; } // 1 - Ativo ; 0 - Inativo
        public string Senha { get; set; }
    }
}
