using ProdutosReactAPI.Dominio.Notifications;

namespace ProdutosReactAPI.Dominio.Entidades
{
    public class Usuario : Notificavel
    {
        public Guid IdUsuario { get; set; } = Guid.NewGuid();
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime DataInclusao { get; set; } = DateTime.UtcNow;

        public Usuario() { }
        public Usuario(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login))
                AdicionarNotificacao(nameof(Usuario), "O login não pode ser vazio.");

            if (login.Length > 50)
                AdicionarNotificacao(nameof(Usuario), "O login não pode ter mais de 50 caracteres.");

            if (string.IsNullOrWhiteSpace(senha))
                AdicionarNotificacao(nameof(Usuario), "A senha não pode ser vazia.");

            Login = login;
            Senha = senha;
        }
    }
}
