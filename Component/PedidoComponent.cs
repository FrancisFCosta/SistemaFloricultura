using Business;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    public class PedidoComponent
    {
        #region Propriedades
        private PedidoBLL PedidoBll;
        #endregion

        #region Construtores
        public PedidoComponent()
        {
            PedidoBll = new PedidoBLL();
        }
        #endregion

        #region Metodos Publicos

        public void RegistrarPedido(Pedido produto)
        {
            PedidoBll.RegistrarPedido(produto);
        }

        public List<Pedido> ListarPedidosPorNome(string nomePedido)
        {
            return PedidoBll.ListarPedidosPorNome(nomePedido);
        }
        #endregion
    }
}
