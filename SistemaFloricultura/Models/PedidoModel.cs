using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaFloricultura.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public List<LinhaPedidoModel> LinhasPedido { get; set; }

        [Display(Name = "Situação")]
        public StatusPedido StatusPedido { get; set; }

        [Display(Name = "Resumo")]
        public String Resumo { get; set; }

        [Display(Name = "Observação")]
        public String Observacao { get; set; }

        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; }

        [Display(Name = "Valor Total")]
        public Double ValorTotal { get; set; }

        public PedidoModel()
        {
            LinhasPedido = new List<LinhaPedidoModel>();
        }
    }
}