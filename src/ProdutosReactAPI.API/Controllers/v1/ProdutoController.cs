using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Produtos;
using ProdutosReactAPI.Aplicacao.Interfaces.Services;

namespace ProdutosReactAPI.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] CriarProdutoDto dto)
        {
            var resultado = await _produtoService.CriarAsync(dto);
            return resultado.Sucesso ? Created("", resultado.Dados) : BadRequest(resultado.Erros);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos([FromQuery] FiltroDto filtro)
        {
            var produtos = await _produtoService.ObterTodosAsync(filtro);
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var resultado = await _produtoService.ObterPorIdAsync(id);
            return resultado.Sucesso ? Ok(resultado.Dados) : NotFound(resultado.Erros);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] CriarProdutoDto dto)
        {
            var resultado = await _produtoService.AtualizarAsync(id, dto);
            return resultado.Sucesso ? NoContent() : BadRequest(resultado.Erros);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultado = await _produtoService.ExcluirAsync(id);
            return resultado.Sucesso ? NoContent() : BadRequest(resultado.Erros);
        }

        [HttpGet("exportar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExportarParaExcel()
        {
            var bytes = await _produtoService.ExportarProdutosParaExcelAsync();

            return File(bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"produtos_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
    }
}
