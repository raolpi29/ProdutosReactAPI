using ProdutosReactAPI.Dominio.Filtros;

namespace ProdutosReactAPI.Aplicacao.Dtos
{
    public class FiltroDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; } = null;
        public bool SortDescending { get; set; } = false;

        public Filtro ToFiltro()
        {
            return new Filtro(Page, PageSize, Sort, SortDescending);
        }
    }
}
