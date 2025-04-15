using ProdutosReactAPI.Dominio.Notifications;

namespace ProdutosReactAPI.Aplicacao.Dtos
{
    public class ResultPaginado<T>
    {
        public bool Sucesso { get; }
        public Paginado<T> Dados { get; }
        public IReadOnlyCollection<Notificacao> Erros { get; }

        private ResultPaginado(Paginado<T> dados, IReadOnlyCollection<Notificacao> erros)
        {
            Sucesso = erros == null || !erros.Any();
            Dados = dados;
            Erros = erros ?? [];
        }

        public static ResultPaginado<T> Ok(Paginado<T> dados) => new(dados, []);
        public static ResultPaginado<T> Falha(IReadOnlyCollection<Notificacao> erros) => new(null, erros);
    }
}
