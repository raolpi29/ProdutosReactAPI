using AutoMapper;
using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Produtos;
using ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Excel;
using ProdutosReactAPI.Aplicacao.Interfaces.Services;
using ProdutosReactAPI.Dominio.Contratos.Repositorios;
using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Aplicacao.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;

        public ProdutoService(IProdutoRepositorio repositorio, IMapper mapper, IExcelService excelService)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _excelService = excelService;
        }

        public async Task<Result<ProdutoDto>> CriarAsync(CriarProdutoDto dto)
        {
            var produto = new Produto(dto.Nome, dto.Valor);

            if (!produto.EhValido)
                return Result<ProdutoDto>.Falha(produto.Notificacoes);

            await this._repositorio.AdicionarAsync(produto);

            return Result<ProdutoDto>.Ok(this._mapper.Map<ProdutoDto>(produto));
        }

        public async Task<Result<ProdutoDto>> ObterPorIdAsync(Guid id)
        {
            var produto = await this._repositorio.ObterPorIdAsync(id);

            if (produto is null)
                return Result<ProdutoDto>.Falha([new("Produto", "Produto não encontrado.")]);

            return Result<ProdutoDto>.Ok(this._mapper.Map<ProdutoDto>(produto));
        }

        public async Task<ResultPaginado<ProdutoDto>> ObterTodosAsync(FiltroDto filtroDto)
        {
            var filtro = filtroDto.ToFiltro();

            var (produtos, totalItens) = await _repositorio.ObterTodosAsync(filtro);

            var paginado = new Paginado<ProdutoDto>
            {
                Itens = _mapper.Map<IEnumerable<ProdutoDto>>(produtos),
                TotalItens = totalItens,
                PaginaAtual = filtro.Page,
                TamanhoPagina = filtro.PageSize
            };

            return ResultPaginado<ProdutoDto>.Ok(paginado);
        }

        public async Task<Result<ProdutoDto>> AtualizarAsync(Guid id, CriarProdutoDto dto)
        {
            var produto = new Produto(dto.Nome, dto.Valor)
            {
                IdProduto = id
            };

            if (!produto.EhValido)
                return Result<ProdutoDto>.Falha(produto.Notificacoes);

            await this._repositorio.AtualizarAsync(produto);

            return Result<ProdutoDto>.Ok(this._mapper.Map<ProdutoDto>(produto));
        }

        public async Task<Result<bool>> ExcluirAsync(Guid id)
        {
            var produto = await this._repositorio.ObterPorIdAsync(id);

            if (produto is null)
                return Result<bool>.Falha([new("Produto", "Produto não encontrado.")]);

            await this._repositorio.ExcluirAsync(id);

            return Result<bool>.Ok(true);
        }

        public async Task<byte[]> ExportarProdutosParaExcelAsync()
        {
            var filtro = new Dominio.Filtros.Filtro(1, int.MaxValue, null, false);

            var (produtos, totalItens) = await _repositorio.ObterTodosAsync(filtro);
            var dtos = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);

            return _excelService.GerarExcel(dtos, "Produtos");
        }
    }
}
