using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFloricultura.Models
{
    public class LinhaPedidoModel
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}