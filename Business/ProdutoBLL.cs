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
        private ImagemProdutoDAL ImagemDAL;
        #endregion

        #region Construtores
        public ProdutoBLL()
        {
            ProdutoDAL = new ProdutoDAL();
            ImagemDAL = new ImagemProdutoDAL();
        }
        #endregion

        #region Metodos Publicos
        public void RegistrarProduto(Produto produto)
        {
            int? idProduto;
            idProduto = ProdutoDAL.Inserir(produto);
            if (idProduto.HasValue && produto.ImagemPrincipal != null)
            {
                produto.ImagemPrincipal.IdProduto = idProduto.Value; 
                ImagemDAL.Inserir(produto.ImagemPrincipal);
            }
        }

        public List<Produto> ListarProdutosPorNome(string nomeProduto)
        {
            return ProdutoDAL.ListarProdutosPorNome(nomeProduto);
        }

        #endregion        
    }
}
