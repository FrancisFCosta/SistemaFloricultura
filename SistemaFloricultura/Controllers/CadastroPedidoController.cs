using Component;
using Entidades;
using SistemaFloricultura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        public ActionResult Index(int? idPedido = null, List<int> listaIdProdutos = null)
        {
            PedidoModel carrinho = Session["Carrinho"] != null ? (PedidoModel)Session["Carrinho"] : new PedidoModel();
            
            return View(carrinho);
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

        public JsonResult AdicionarCarrinho(int idProduto)
        {
            PedidoModel carrinho = Session["Carrinho"] != null ? (PedidoModel)Session["Carrinho"] : new PedidoModel();

            var produto = ProdutoComponent.ObterProdutoPorID(idProduto);

            if (produto == null)
            {
                return Json(new { sucesso = false, mensagem = "Não foi possível adicionar o produto ao carrinho. O produto não foi encontrado." }, JsonRequestBehavior.AllowGet);
            }

            var linhaPedido = new LinhaPedidoModel();
                linhaPedido.Produto = produto;
                linhaPedido.Quantidade = 1;
                linhaPedido.DataInclusao = DateTime.Now;

                if (carrinho.LinhasPedido.FirstOrDefault(linha => linha.Produto != null && linha.Produto.Id == produto.Id) != null)
                {
                    carrinho.LinhasPedido.FirstOrDefault(linha => linha.Produto != null && linha.Produto.Id == produto.Id).Quantidade += 1;
                }

                else
                {
                    carrinho.LinhasPedido.Add(linhaPedido);
                }

                carrinho.ValorTotal = carrinho.LinhasPedido.Select(i => i.Produto).Sum(d => d.PrecoVenda);

                Session["Carrinho"] = carrinho;

            return Json(new { sucesso = true, mensagem = "Produto adicionado com sucesso. Para conferir, verifique seu carrinho." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirItem(int idProduto)
        {
            var carrinho = Session["Carrinho"] != null ? (PedidoModel)Session["Carrinho"] : new PedidoModel();
            var itemExclusao = carrinho.LinhasPedido.FirstOrDefault(i => i.Produto!= null && i.Produto.Id == idProduto);
            carrinho.LinhasPedido.Remove(itemExclusao);

            Session["Carrinho"] = carrinho;
            return Json(new { sucesso = true, mensagem = "Produto removido com sucesso. Para conferir, verifique seu carrinho." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalvarCarrinho()
        {
            var carrinho = Session["Carrinho"] != null ? (Pedido)Session["Carrinho"] : new Pedido();

            //db.Pedidos.Add(carrinho);
            //db.SaveChanges();

            return RedirectToAction("Carrinho");
        }
    }
}