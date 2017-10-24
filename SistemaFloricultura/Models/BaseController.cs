using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFloricultura.Models
{
    public class BaseController : Controller
    {
        public static Produto ConstruirProduto(ProdutoModel produto, HttpPostedFileBase imagem)
        {
            if (produto != null)
            {
                ImagemProduto imagemSalva = null;

                if (imagem != null)
                {
                    MemoryStream target = new MemoryStream();
                    imagem.InputStream.CopyTo(target);
                    byte[] imagemBytes = target.ToArray();

                    imagemSalva = new ImagemProduto()
                    {
                        NomeImagem = imagem.FileName,
                        ImagemBytes = imagemBytes,
                        TamanhoImagem = imagem.ContentLength,
                        IdcPrincipal = true
                    };
                }

                return new Produto()
                {
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    PrecoVenda = produto.PrecoVenda,
                    PrecoCusto = produto.PrecoCusto,
                    DataAquisicao = produto.DataAquisicao,
                    ImagemPrincipal = imagemSalva
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
                        pedidoEntidade.ListaProdutos.Add(ConstruirProduto(produto,null));
                }
            }
            return pedidoEntidade;
        }
    }
}