using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.DTOs;
using SistemaEstoque.Models;
using SistemaEstoque.Services;


namespace SistemaEstoque.Controllers;

[ApiController]
[Route("[Controller]")]
public class ProdutoController : ControllerBase
    {
    private readonly ProdutoService _Service;
    private readonly IMapper _mapper;

    public ProdutoController(ProdutoService service, IMapper mapper)
        {
        _Service = service;
        _mapper = mapper;
        }

    [HttpGet]
    public async Task<List<ProdutoDto>> BuscarProduto()
        {
        var produtos = await _Service.BuscarProdutos();

        return _mapper.Map<List<ProdutoDto>>(produtos);
        }
    [HttpPost]
    public async Task<IActionResult> CadastrarProduto([FromBody] Produto produto)
        {
        await _Service.CadastrarProduto(produto);

        return StatusCode(201);
        }

    [HttpPut("{id}")]
    public async Task<IActionResult> AlterarProduto(int id, [FromBody] Produto produto)
        {
        await _Service.AlterarProduto(id, produto);

        return StatusCode(204);
        }
    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirProduto(int id)
        {
        await _Service.ExcluirProduto(id);

        return NoContent();
        }
    }






