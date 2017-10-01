using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProdutoDAL : DBConnection
    {
        #region Constantes

        private const string INSERIR = @"
            INSERT INTO produto 
            (
                `id_produto`,
                `nome_produto`,
                `descricao_produto`, 
                `preco_custo`, 
                `preco_venda`, 
                `data_aquisicao`
            ) 
            VALUES 
            (
                @id_produto, 
                @nome_produto, 
                @descricao_produto, 
                @preco_custo, 
                @preco_venda, 
                @data_aquisicao
            );";
        private const string LISTAR_PRODUTO_POR_NOME = "SELECT * FROM cliente WHERE nome LIKE '%{0}%';";

        #endregion

        #region Metodos Publicos

        public List<Produto> ListarProdutosPorNome(string nomeProduto)
        {
            var dbCon = DBConnection.Instance();
            List<Produto> listaRetorno = new List<Produto>();

            if (dbCon.IsConnect())
            {
                string query = String.Format(LISTAR_PRODUTO_POR_NOME, nomeProduto);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaRetorno.Add(ConstruirProduto(reader));
                }

                reader.Close();
                cmd.Dispose();
            }
            return listaRetorno;
        }

        public void Inserir(Produto Produto)
        {
            var dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(INSERIR, dbCon.Connection);
                var param1 = new MySqlParameter("@nome_produto", MySqlDbType.Int32);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        #endregion

        #region Metodos Privados

        private Produto ConstruirProduto(MySqlDataReader reader)
        {
            return new Produto()
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Descricao = reader.GetString(2),
                PrecoCusto = reader.GetDouble(3),
                PrecoVenda = reader.GetDouble(4),
                DataAquisicao = reader.GetDateTime(5)
            };
        }

        #endregion
    }
}
