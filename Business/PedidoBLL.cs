using DataAccess;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PedidoBLL
    {
        #region Propriedades
        private PedidoDAL PedidoDAL;
        #endregion

        #region Construtores
        public PedidoBLL()
        {
            PedidoDAL = new PedidoDAL();
        }
        #endregion

        #region Metodos Publicos
        public void RegistrarPedido(Pedido produto)
        {
            PedidoDAL.Inserir(produto);
        }

        public List<Pedido> ListarPedidosPorNome(string nomePedido)
        {
            return PedidoDAL.ListarPedidosPorNome(nomePedido);
        }

        #endregion        
    }
}
