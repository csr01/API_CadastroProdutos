using CadastroProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.Filtros
{
    public class Paginacao
    {
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int NumeroPagina { get; set; }
        public IList<Produto> ListaItens{ get; set; }

        public static Paginacao From(ProdutoPaginacao parametros, IQueryable<Produto> origens)
        {
            if (parametros == null)
            {
                parametros = new ProdutoPaginacao();
            }
            int total = origens.Count();
            int totalPaginas = (int)Math.Ceiling(total / (double)parametros.Tamanho);
            return new Paginacao
            {
                TotalItens = total,
                TotalPaginas = totalPaginas,
                TamanhoPagina = parametros.Tamanho,
                NumeroPagina = parametros.Pagina,
                ListaItens = origens.Skip(parametros.QtdeParaDescartar).Take(parametros.Tamanho).ToList()
            };
        }
    }

    public class ProdutoPaginacao
    {
        private readonly int TAMANHO_PADRAO = 25;
        private int _pagina = 0;
        private int _tamanho = 0;

        public int Pagina
        {
            get
            {
                return (_pagina <= 0) ? 1 : _pagina;
            }
            set
            {
                _pagina = value;
            }
        }
        public int Tamanho
        {
            get
            {
                return (_tamanho <= 0) ? TAMANHO_PADRAO : _tamanho;
            }
            set
            {
                _tamanho = value;
            }
        }

        public int QtdeParaDescartar => Pagina > 0 ? Tamanho * (Pagina - 1) : Tamanho;
    }
}
