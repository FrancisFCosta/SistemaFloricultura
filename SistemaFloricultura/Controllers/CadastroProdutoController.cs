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
    public class CadastroProdutoController : Controller
    {
        #region Metodos Publicos

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalvarProduto(ProdutoModel produto)
        {
            if (produto != null)
            {
                ProdutoComponent produtoComponent = new ProdutoComponent();

                produtoComponent.RegistrarProduto(ConstruirProduto(produto));

                List<Produto> listaUsuarios = produtoComponent.ListarProdutosPorNome("Sabonete");
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Metodos Privados

        #endregion
        private static Produto ConstruirProduto(ProdutoModel produto)
        {
            if (produto != null)
            {
                return new Produto()
                {
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    PrecoVenda = produto.PrecoVenda,
                    PrecoCusto = produto.PrecoCusto,
                    DataAquisicao = produto.DataAquisicao
                };
            }
            return null;
        }
    }
}