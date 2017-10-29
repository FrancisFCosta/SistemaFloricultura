using DataAccess;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class ProdutoBLL
    {
        #region Propriedades

        private ProdutoDAL ProdutoDAL;
        private ImagemProdutoDAL ImagemDAL;
        private CategoriaDAL CategoriaDAL;

        #endregion

        #region Construtores

        public ProdutoBLL()
        {
            ProdutoDAL = new ProdutoDAL();
            ImagemDAL = new ImagemProdutoDAL();
            CategoriaDAL = new CategoriaDAL();
        }

        #endregion

        #region Metodos Publicos

        public void RegistrarProduto(Produto produto)
        {
            int? idProduto;
            idProduto = ProdutoDAL.Inserir(produto);

            if (!idProduto.HasValue)
                return;

            if (produto.ImagemPrincipal != null)
            {
                produto.ImagemPrincipal.IdProduto = idProduto.Value;
                ImagemDAL.Inserir(produto.ImagemPrincipal);
            }

            if (produto.ListaCategorias != null && produto.ListaCategorias.Any())
            {
                foreach (var categoria in produto.ListaCategorias)
                    CategoriaDAL.Inserir(new Categoria() { IdProduto = idProduto.Value, CodigoCategoria = categoria });
            }
        }

        public void AtualizarProduto(Produto produto)
        {
            ProdutoDAL.Atualizar(produto);

            ImagemDAL.ExcluirPorId(produto.Id);

            if (produto.ImagemPrincipal != null)
            {
                produto.ImagemPrincipal.IdProduto = produto.Id;
                ImagemDAL.Inserir(produto.ImagemPrincipal);
            }

            CategoriaDAL.ExcluirPorId(produto.Id);

            if (produto.ListaCategorias != null && produto.ListaCategorias.Any())
            {
                foreach (var categoria in produto.ListaCategorias)
                    CategoriaDAL.Inserir(new Categoria() { IdProduto = produto.Id, CodigoCategoria = categoria });
            }
        }
        public Produto ObterProdutoPorID(int idProduto)
        {
            Produto produto = ProdutoDAL.ObterProdutoPorID(idProduto);

            if (produto != null)
            {
                produto.ImagemPrincipal = ImagemDAL.ObterImagemPrincipal(produto.Id);
                List<Categoria> listaCategorias = CategoriaDAL.ListarCategoriasPorId(produto.Id);
                produto.ListaCategorias = listaCategorias != null && listaCategorias.Any() ? listaCategorias.Select(cat => cat.CodigoCategoria).ToList() : new List<CategoriaProduto>();
            }

            return produto;
        }

        public List<Produto> ListarProdutos()
        {
            List<Produto> listaRetorno = ProdutoDAL.ListarProdutos();

            if (listaRetorno != null && listaRetorno.Any())
            {
                foreach (var produto in listaRetorno)
                    produto.ImagemPrincipal = ImagemDAL.ObterImagemPrincipal(produto.Id);
            }

            return listaRetorno;
        }

        public List<Produto> ListarProdutosPorCategoria(CategoriaProduto categoria)
        {
            List<Produto> listaRetorno = ProdutoDAL.ListarProdutosPorCategoria(categoria);

            if (listaRetorno != null && listaRetorno.Any())
            {
                foreach (var produto in listaRetorno)
                    produto.ImagemPrincipal = ImagemDAL.ObterImagemPrincipal(produto.Id);
            }

            return listaRetorno;
        }
        
        public List<Produto> ListarProdutosPorNome(string nomeProduto)
        {
            return ProdutoDAL.ListarProdutosPorNome(nomeProduto);
        }

        #endregion
    }
}
