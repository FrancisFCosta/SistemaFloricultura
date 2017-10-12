using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFloricultura.Models
{
    public class BaseController : Controller
    {
        public static Produto ConstruirProduto(ProdutoModel produto)
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

        public static Pedido ConstruirPedido(PedidoModel pedido)
        {
            Pedido pedidoEntidade = null;

            if (pedido != null)
            {
                pedidoEntidade = new Pedido();

                if (pedido.ListaProdutos.Any())
                {
                    foreach (var produto in pedido.ListaProdutos)
                        pedidoEntidade.ListaProdutos.Add(ConstruirProduto(produto));
                }
            }
            return pedidoEntidade;
        }
    }
}