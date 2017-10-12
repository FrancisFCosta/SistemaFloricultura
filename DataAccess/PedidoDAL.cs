using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PedidoDAL : DBConnection
    {
        #region Constantes

        private const string INSERIR = @"
            INSERT INTO produto 
            (
                `nome_produto`,
                `descricao_produto`, 
                `preco_custo`, 
                `preco_venda`, 
                `data_aquisicao`
            ) 
            VALUES 
            (
                @nome_produto, 
                @descricao_produto, 
                @preco_custo, 
                @preco_venda, 
                @data_aquisicao
            );";
        private const string LISTAR_PRODUTO_POR_NOME = "SELECT * FROM produto WHERE nome_produto LIKE '%{0}%';";

        #endregion

        #region Metodos Publicos

        public List<Pedido> ListarPedidosPorNome(string nomePedido)
        {
            var dbCon = DBConnection.Instance();
            List<Pedido> listaRetorno = new List<Pedido>();

            if (dbCon.IsConnect())
            {
                string query = String.Format(LISTAR_PRODUTO_POR_NOME, nomePedido);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        listaRetorno.Add(ConstruirPedido(reader));
                    }
                }
                catch (Exception)
                {
                    throw;
                }                    
                finally
                {
                    reader.Close();
                    cmd.Dispose();
                }
            }
            return listaRetorno;
        }

        public void Inserir(Pedido produto)
        {
            var dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(INSERIR, dbCon.Connection);

                //cmd.Parameters.AddWithValue("@nome_produto", produto.Nome);
                //cmd.Parameters.AddWithValue("@descricao_produto", produto.Descricao);
                //cmd.Parameters.AddWithValue("@preco_custo", produto.PrecoCusto);
                //cmd.Parameters.AddWithValue("@preco_venda", produto.PrecoVenda);
                //cmd.Parameters.AddWithValue("@data_aquisicao", produto.DataAquisicao);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        #endregion

        #region Metodos Privados

        private Pedido ConstruirPedido(MySqlDataReader reader)
        {
            return new Pedido()
            {
                //Id = reader.GetInt32(0),
                //Nome = reader.GetString(1),
                //Descricao = reader.GetString(2),
                //PrecoCusto = reader.GetDouble(3),
                //PrecoVenda = reader.GetDouble(4),
                //DataAquisicao = reader.GetDateTime(5)
            };
        }

        #endregion
    }
}
