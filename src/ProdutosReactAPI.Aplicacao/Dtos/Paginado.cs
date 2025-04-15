using ProdutosReactAPI.Dominio.Notifications;

namespace ProdutosReactAPI.Aplicacao.Dtos
{
    public class Paginado<T>
    {
        public IEnumerable<T> Itens { get; set; } = Enumerable.Empty<T>();
        public int TotalItens { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }

        public int TotalPaginas =>
            (int)Math.Ceiling((double)TotalItens / TamanhoPagina);
    }
}
