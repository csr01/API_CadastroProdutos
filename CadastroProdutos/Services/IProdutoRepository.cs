using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.Models
{
    public interface IProdutoRepository
    {
        IQueryable<Produto> AllProdutos { get; }
        Produto ProdutoByCodigo(int id);
        void InsertProduto(Produto produto);
        void DeleteProduto(Produto p);
        void UpdateProduto(Produto produto);
    }
}
