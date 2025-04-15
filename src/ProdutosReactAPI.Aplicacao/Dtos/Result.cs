using ProdutosReactAPI.Dominio.Notifications;

namespace ProdutosReactAPI.Aplicacao.Dtos
{
    public class Result<T>
    {
        public bool Sucesso { get; }
        public T Dados { get; }
        public IReadOnlyCollection<Notificacao> Erros { get; }

        private Result(T dados, IReadOnlyCollection<Notificacao> erros)
        {
            Sucesso = erros == null || !erros.Any();
            Dados = dados;
            Erros = erros ?? [];
        }

        public static Result<T> Ok(T dados) => new(dados, []);
        public static Result<T> Falha(IReadOnlyCollection<Notificacao> erros) => new(default, erros);
    }
}
