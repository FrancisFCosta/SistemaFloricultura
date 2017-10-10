using DataAccess;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ProdutoBLL
    {
        #region Propriedades
        private ProdutoDAL ProdutoDAL;
        #endregion

        #region Construtores
        public ProdutoBLL()
        {
            ProdutoDAL = new ProdutoDAL();
        }
        #endregion

        #region Metodos Publicos
        public void RegistrarProduto(Produto produto)
        {
            ProdutoDAL.Inserir(produto);
        }

        public List<Produto> ListarProdutosPorNome(string nomeProduto)
        {
            return ProdutoDAL.ListarProdutosPorNome(nomeProduto);
        }

        #endregion        
    }
}
