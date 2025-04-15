using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Dominio.Contratos.Repositorios;
using ProdutosReactAPI.Dominio.Entidades;
using ProdutosReactAPI.Dominio.Filtros;
using ProdutosReactAPI.Persistencia.Contexto;

namespace ProdutosReactAPI.Persistencia.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly AppDbContext _context;

        public ProdutoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(Guid id)
        {
            var produto = await ObterPorIdAsync(id);
            if (produto is null) return;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto?> ObterPorIdAsync(Guid id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.IdProduto == id);
        }

        public async Task<(IEnumerable<Produto>, int)> ObterTodosAsync(Filtro filtro)
        {
            IQueryable<Produto> query = _context.Produtos;

            query = filtro.Sort?.ToLower() switch
            {
                "valor" => filtro.SortDescending ? query.OrderByDescending(p => p.Valor) : query.OrderBy(p => p.Valor),
                "datainclusao" => filtro.SortDescending ? query.OrderByDescending(p => p.DataInclusao) : query.OrderBy(p => p.DataInclusao),
                _ => filtro.SortDescending ? query.OrderByDescending(p => p.Nome) : query.OrderBy(p => p.Nome)
            };

            var total = await query.CountAsync();

            var itens = await query
                .Skip((filtro.Page - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .ToListAsync();

            return (itens, total);
        }
    }
}
