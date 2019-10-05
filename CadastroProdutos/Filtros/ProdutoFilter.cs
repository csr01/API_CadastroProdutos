using CadastroProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.Filtros
{
    public static class ProdutoExtensions
    {
        public static IQueryable<Produto> AplicaFiltro(this IQueryable<Produto> query, ProdutoFilter produtoFilter)
        {
            if (produtoFilter != null)
            {
                if (!string.IsNullOrEmpty(produtoFilter.Descricao))
                {
                    query = query.Where(p => p.Descricao.Contains(produtoFilter.Descricao));
                }

                if (!string.IsNullOrEmpty(produtoFilter.Fabricante))
                {
                    query = query.Where(p => p.Fabricante.Contains(produtoFilter.Fabricante));
                }
            }
            return query;
        }
    }

    public class ProdutoFilter
    {
        public string Descricao { get; set; }
        public string Fabricante { get; set; }
    }
}
