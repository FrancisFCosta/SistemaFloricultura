using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LinhaPedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
