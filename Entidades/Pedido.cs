using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public List<LinhaPedido> LinhasPedido { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public String Resumo { get; set; }
        public String Observacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Double ValorTotal { get; set; }

        public Pedido()
        {
            LinhasPedido = new List<LinhaPedido>();
        }
    }
}
