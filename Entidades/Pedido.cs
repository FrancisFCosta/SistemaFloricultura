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
        public List<Produto> ListaProdutos { get; set; }

        public Pedido()
        {
            ListaProdutos = new List<Produto>();
        }
    }
}
