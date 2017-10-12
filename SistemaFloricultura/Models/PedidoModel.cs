using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFloricultura.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public List<ProdutoModel> ListaProdutos { get; set; }

        public PedidoModel()
        {
            ListaProdutos = new List<ProdutoModel>();
        }
    }
}