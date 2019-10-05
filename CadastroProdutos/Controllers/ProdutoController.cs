using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroProdutos.Filtros;
using CadastroProdutos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutos.Controllers
{
    [Authorize(Roles ="usuario")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repo;

        public ProdutoController(IProdutoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult AllProdutos([FromQuery] ProdutoFilter filtro,
                                         [FromQuery] ProdutoOrdem ordem,
                                         [FromQuery] ProdutoPaginacao paginacao)
        {
            var lista = _repo.AllProdutos
                        .AplicaFiltro(filtro)
                        .AplicaOrdenacao(ordem);

            var listaPaginada = Paginacao.From(paginacao, lista);

            if(listaPaginada.ListaItens.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaPaginada);
        }

        [HttpGet("{id}")]
        public IActionResult ProdutoById(int id)
        {
            var produto = _repo.ProdutoByCodigo(id);

            if(produto != null)
            {
                return Ok(produto);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult InsertProduto([FromBody]Produto p)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertProduto(p);
                return CreatedAtAction("AllProdutos", new { id = p.Id }, p);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateProduto([FromBody]ProdutoDTO p)
        {
            if (ModelState.IsValid)
            {
                var pteste = p.ToModel();
                _repo.UpdateProduto(pteste);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = _repo.ProdutoByCodigo(id);
            if(produto == null)
            {
                return NotFound();
            }
            _repo.DeleteProduto(produto);
            return NoContent();
        }
    }


    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public string Fabricante { get; set; }
        public DateTime Validade { get; set; }
    }

    public static class ProdutoExtensions
    {
        public static Produto ToModel(this ProdutoDTO produtoDTO)
        {
            Produto p = new Produto
            {
                Id = produtoDTO.Id,
                Descricao = produtoDTO.Descricao,
                Fabricante = produtoDTO.Fabricante,
                PrecoCusto = produtoDTO.PrecoCusto,
                PrecoVenda = produtoDTO.PrecoVenda,
                Validade = produtoDTO.Validade
            };

            return p;
        }
    }
}