
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.DTOs;
using SistemaEstoque.Models;
using SistemaEstoque.Services;

namespace SistemaEstoque.Controllers
    {
    [ApiController]
    [Route("[Controller]")]
    public class ProdutoController : ControllerBase
        {
        private readonly ProdutoService _service;
        private readonly IMapper _mapper;

        public ProdutoController(ProdutoService service, IMapper mapper)
            {
            _service = service;
            _mapper = mapper;
            }

        [HttpGet]
        public async Task<List<ProdutoDto>> BuscarProduto()
            {
            var produtos = await _service.BuscarProdutos();

            return _mapper.Map<List<ProdutoDto>>(produtos);
            }

        [HttpPost]
        public async Task<IActionResult> CadastrarProduto([FromBody] Produto produto)
            {
            await _service.CadastrarProduto(produto);

            return StatusCode(201);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarProduto(int id, [FromBody] Produto produto)
            {
            await _service.AlterarProduto(id, produto);

            return NoContent();
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProduto(int id)
            {
            await _service.ExcluirProduto(id);

            return NoContent();
            }
        }
    }


