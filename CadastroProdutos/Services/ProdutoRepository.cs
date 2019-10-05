using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.Models
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProgramContext _context;

        public ProdutoRepository(ProgramContext context)
        {
            _context = context;
        }

        public IQueryable<Produto> AllProdutos => _context.Set<Produto>().AsQueryable();

        public void DeleteProduto(Produto p)
        {
            _context.Produtos.Remove(p);
            _context.SaveChanges();
        }

        public void InsertProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public Produto ProdutoByCodigo(int id)
        {
            return _context.Produtos.Find(id);
        }

        public IList<Produto> ProdutoByFornecedor(string fabricante)
        {
            return _context.Produtos.Where(p =>
                p.Fabricante.Contains(fabricante, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public IList<Produto> ProdutoByName(string name)
        {
            return _context.Produtos.Where(p =>
                p.Descricao.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public void UpdateProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }
    }
}
