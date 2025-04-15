namespace ProdutosReactAPI.Dominio.Filtros
{
    public class Filtro
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; } = null;
        public bool SortDescending { get; set; } = false;

        public Filtro(int page, int pageSize, string? sort, bool sortDescending)
        {
            Page = page;
            PageSize = pageSize;
            Sort = sort;
            SortDescending = sortDescending;
        }
    }
}
