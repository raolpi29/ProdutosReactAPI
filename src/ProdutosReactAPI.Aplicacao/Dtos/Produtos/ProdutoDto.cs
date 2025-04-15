namespace ProdutosReactAPI.Aplicacao.Dtos.Produtos
{
    public class ProdutoDto
    {
        public Guid IdProduto { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
