using ProdutosReactAPI.Dominio.Notifications;

namespace ProdutosReactAPI.Dominio.Entidades
{
    public class Produto : Notificavel
    {
        public Guid IdProduto { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.UtcNow;

        public Produto() { }
        public Produto(string nome, decimal valor)
        {
            if (string.IsNullOrWhiteSpace(nome))
                AdicionarNotificacao(nameof(Produto), "O nome não pode ser vazio.");

            if (nome.Length > 150)
                AdicionarNotificacao(nameof(Produto), "O nome não pode ter mais de 150 caracteres.");

            if (valor <= 0)
                AdicionarNotificacao(nameof(Produto), "O valor deve ser maior que zero.");

            Nome = nome;
            Valor = valor;
        }
    }
}
