using Business;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    public class ProdutoComponent
    {
        #region Propriedades
        private ProdutoBLL ProdutoBll;
        #endregion

        #region Construtores
        public ProdutoComponent()
        {
            ProdutoBll = new ProdutoBLL();
        }
        #endregion

        #region Metodos Publicos

        public Produto ObterProdutoPorID(int idProduto)
        {
            return ProdutoBll.ObterProdutoPorID(idProduto);
        }

        public void RegistrarProduto(Produto produto)
        {
            ProdutoBll.RegistrarProduto(produto);
        }
        public void AtualizarProduto(Produto produto)
        {
            ProdutoBll.AtualizarProduto(produto);
        }

        public List<Produto> ListarProdutos()
        {
            return ProdutoBll.ListarProdutos();
        }

        public List<Produto> ListarProdutosPorCategoria(CategoriaProduto categoria)
        {
            return ProdutoBll.ListarProdutosPorCategoria(categoria);
        }

        public List<Produto> ListarProdutosPorNome(string nomeProduto)
        {
            return ProdutoBll.ListarProdutosPorNome(nomeProduto);
        }
        #endregion
    }
}
