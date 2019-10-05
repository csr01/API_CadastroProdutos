using CadastroProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace CadastroProdutos.Filtros
{
    public static class ProdutoOrdemExtensions
    {
        public static IQueryable<Produto> AplicaOrdenacao(this IQueryable<Produto> query, ProdutoOrdem ordem)
        {
            if (!string.IsNullOrEmpty(ordem.OrdenaPor))
            {
                query = query.OrderBy(ordem.OrdenaPor);
            }
            return query;
        }
    }
    public class ProdutoOrdem
    {
        public string OrdenaPor { get; set; }
    }
}
