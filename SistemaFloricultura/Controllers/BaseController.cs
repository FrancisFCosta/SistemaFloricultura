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
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    PrecoVenda = produto.PrecoVenda,
                    PrecoCusto = produto.PrecoCusto,
                    DataAquisicao = produto.DataAquisicao,
                    ListaCategorias = produto.LstCategorias,
                    ImagemPrincipal = imagemSalva
                };
            }
            return null;
        }

        public static ProdutoModel ConstruirProdutoModel(Produto produto)
        {
            if (produto != null)
            {
                return new ProdutoModel()
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    PrecoVenda = produto.PrecoVenda,
                    PrecoCusto = produto.PrecoCusto,
                    DataAquisicao = produto.DataAquisicao,
                    LstCategorias = produto.ListaCategorias,
                    ImagemPrincipal = produto.ImagemPrincipal
                };
            }
            return new ProdutoModel();
        }

        public static List<ProdutoModel> ObterListaProdutoModel(List<Produto> list)
        {
            List<ProdutoModel> listaRetorno = new List<ProdutoModel>();

            if (list == null || !list.Any())
                return listaRetorno;

            foreach (var produto in list)
                listaRetorno.Add(ConstruirProdutoModel(produto));

            return listaRetorno;
        }

        public static Pedido ConstruirPedido(PedidoModel pedido)
        {
            Pedido pedidoEntidade = null;

            if (pedido != null)
            {
                pedidoEntidade = new Pedido();

                //if (pedido.ListaProdutos.Any())
                //{
                //    foreach (var produto in pedido.ListaProdutos)
                //        pedidoEntidade.ListaProdutos.Add(ConstruirProduto(produto,null));
                //}
            }
            return pedidoEntidade;
        }

        public static PedidoModel ConstruirPedidoModel(Pedido pedido)
        {
            if (pedido != null)
            {
                return new PedidoModel()
                {
                    Id = pedido.Id,
                    Resumo = pedido.Resumo,
                    IdCliente = pedido.IdCliente,
                    LinhasPedido = ObterListaLinhaPedidoModel(pedido.LinhasPedido),
                    Observacao = pedido.Observacao,
                    DataAlteracao = pedido.DataAlteracao,
                    StatusPedido = pedido.StatusPedido
                };
            }
            return new PedidoModel();
        }

        public static List<LinhaPedidoModel> ObterListaLinhaPedidoModel(List<LinhaPedido> list)
        {
            List<LinhaPedidoModel> listaRetorno = new List<LinhaPedidoModel>();

            if (list == null || !list.Any())
                return listaRetorno;

            foreach (var linhaPedido in list)
                listaRetorno.Add(ConstruirLinhaPedidoModel(linhaPedido));

            return listaRetorno;
        }

        public static LinhaPedidoModel ConstruirLinhaPedidoModel(LinhaPedido linhaPedido)
        {
            if (linhaPedido != null)
            {
                return new LinhaPedidoModel()
                {
                    DataInclusao = linhaPedido.DataInclusao,
                    IdPedido = linhaPedido.IdPedido,
                    Produto = linhaPedido.Produto,
                    IdCliente = linhaPedido.IdCliente,
                    Quantidade = linhaPedido.Quantidade
                };
            }
            return new LinhaPedidoModel();
        }
    }
}