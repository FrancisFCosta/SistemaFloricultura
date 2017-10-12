﻿using Component;
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalvarProduto(ProdutoModel produto)
        {
            if (produto != null)
            {
                produtoComponent.RegistrarProduto(ConstruirProduto(produto));

                List<Produto> listaUsuarios = produtoComponent.ListarProdutosPorNome("o");
            }

            return RedirectToAction("Index");
        }

        #endregion
        
    }
}