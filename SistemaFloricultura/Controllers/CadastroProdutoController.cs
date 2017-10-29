using Component;
using Entidades;
using SistemaFloricultura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFloricultura.Controllers
{
    public class CadastroProdutoController : BaseController
    {
        #region Propriedades

        ProdutoComponent produtoComponent;

        #endregion

        #region Construtores

        public CadastroProdutoController()
        {
            produtoComponent = new ProdutoComponent();
        }
        #endregion

        #region Metodos Publicos

        public ActionResult Index(int? id_produto = null)
        {
            ProdutoModel produtoModel = new ProdutoModel();

            if (id_produto.HasValue)
            {
                produtoModel = ConstruirProdutoModel(produtoComponent.ObterProdutoPorID(id_produto.Value));
            }
            return View(produtoModel);
        }

        public ActionResult ListagemProdutos(CategoriaProduto? categoriaProduto = null)
        {
            List<ProdutoModel> listaProdutos = null;

            if (categoriaProduto.HasValue)
                listaProdutos = ObterListaProdutoModel(produtoComponent.ListarProdutosPorCategoria(categoriaProduto.Value));
            else
                listaProdutos = ObterListaProdutoModel(produtoComponent.ListarProdutos());

            return View(listaProdutos);
        }

        [HttpPost]
        public ActionResult SalvarProduto(ProdutoModel produto, HttpPostedFileBase ImagemProduto)
        {
            Produto produtoParaSalvar = ConstruirProduto(produto, ImagemProduto);
            Produto produtoNoBanco = produtoComponent.ObterProdutoPorID(produtoParaSalvar.Id);

            if (produtoNoBanco != null)
                produtoComponent.AtualizarProduto(produtoParaSalvar);
            else
                produtoComponent.RegistrarProduto(produtoParaSalvar);

            return RedirectToAction("Index");
        }

        #endregion
        
    }
}