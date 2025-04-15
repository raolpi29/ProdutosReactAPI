namespace ProdutosReactAPI.Dominio.Notifications
{
    public class Notificacao
    {
        public string Propriedade { get; }
        public string Mensagem { get; }

        public Notificacao(string propriedade, string mensagem)
        {
            Propriedade = propriedade;
            Mensagem = mensagem;
        }
    }

    public abstract class Notificavel
    {
        private readonly List<Notificacao> _notificacoes = [];
        public IReadOnlyCollection<Notificacao> Notificacoes => _notificacoes.AsReadOnly();
        public bool EhValido => _notificacoes.Count == 0;

        public void AdicionarNotificacao(string propriedade, string mensagem)
        {
            _notificacoes.Add(new Notificacao(propriedade, mensagem));
        }

        public void AdicionarNotificacao(IEnumerable<Notificacao> notificacoes)
        {
            _notificacoes.AddRange(notificacoes);
        }

        public void LimparNotificacoes()
        {
            _notificacoes.Clear();
        }
    }
}
