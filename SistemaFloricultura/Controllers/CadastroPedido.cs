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
    public class CadastroPedidoController : BaseController
    {
        #region Propriedades

        ProdutoComponent ProdutoComponent;
        PedidoComponent PedidoComponent;
        #endregion

        #region Construtores

        public CadastroPedidoController()
        {
            ProdutoComponent = new ProdutoComponent();
            PedidoComponent = new PedidoComponent();
        }
        #endregion

        #region Metodos Publicos

        public ActionResult Index(CategoriaProduto? categoriaProduto = null)
        {
            List<ProdutoModel> listaProdutos = null;

            if (categoriaProduto.HasValue)
                listaProdutos = ObterListaProdutoModel(ProdutoComponent.ListarProdutosPorCategoria(categoriaProduto.Value));
            else
                listaProdutos = ObterListaProdutoModel(ProdutoComponent.ListarProdutos());

            return View(listaProdutos);
        }
        
        public ActionResult SalvarPedido(PedidoModel produto)
        {
            if (produto != null)
            {
                PedidoComponent produtoComponent = new PedidoComponent();

                produtoComponent.RegistrarPedido(ConstruirPedido(produto));

                List<Pedido> listaUsuarios = produtoComponent.ListarPedidosPorNome("o");
            }

            return RedirectToAction("Index");
        }

        #endregion
        
    }
}