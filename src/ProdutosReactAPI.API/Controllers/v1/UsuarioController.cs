using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;
using ProdutosReactAPI.Aplicacao.Interfaces.Services;

namespace ProdutosReactAPI.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] CriarUsuarioDto dto)
        {
            var resultado = await _usuarioService.CriarAsync(dto);
            return resultado.Sucesso ? Created("", resultado.Dados) : BadRequest(resultado.Erros);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto dto)
        {
            var resultado = await _usuarioService.LoginAsync(dto);
            return resultado.Sucesso ? Ok(resultado.Dados) : Unauthorized(resultado.Erros);
        }
    }
}
