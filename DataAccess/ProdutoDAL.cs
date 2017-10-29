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
                `nome`,
                `descricao`, 
                `preco_custo`, 
                `preco_venda`, 
                `data_aquisicao`
            ) 
            VALUES 
            (
                @nome, 
                @descricao, 
                @preco_custo, 
                @preco_venda, 
                @data_aquisicao
            );";

        private const string ATUALIZAR = @"
            UPDATE `produto` 
            SET  
             `nome`= @nome, 
             `descricao`= @descricao, 
             `preco_custo`= @preco_custo, 
             `preco_venda`= @preco_venda, 
             `data_aquisicao`= @data_aquisicao  
            WHERE `id`= @id;";

        private const string LISTAR_PRODUTOS = "SELECT * FROM produto";
        private const string PRODUTO_POR_ID = "select * from produto where id = @id;";
        private const string LISTAR_PRODUTO_POR_NOME = "SELECT * FROM produto WHERE nome_produto LIKE '%{0}%';";
        private const string LISTAR_PRODUTO_POR_CATEGORIA = "select * from produto inner join categoria on produto.id = categoria.produto_id where categoria.categoria = @categoria;";
        #endregion

        #region Metodos Publicos

        public Produto ObterProdutoPorID(int idProduto)
        {
            var dbCon = DBConnection.Instance();
            Produto produtoRecuperado = null;
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(PRODUTO_POR_ID, dbCon.Connection);

                cmd.Parameters.Add("id", MySqlDbType.Int32).Value = idProduto;

                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        produtoRecuperado = ConstruirProduto(reader);
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
            return produtoRecuperado;
        }

        public List<Produto> ListarProdutos()
        {
            var dbCon = DBConnection.Instance();
            List<Produto> listaRetorno = new List<Produto>();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(LISTAR_PRODUTOS, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        listaRetorno.Add(ConstruirProduto(reader));
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

        public List<Produto> ListarProdutosPorCategoria(CategoriaProduto categoria)
        {
            var dbCon = DBConnection.Instance();
            List<Produto> listaRetorno = new List<Produto>();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(LISTAR_PRODUTO_POR_CATEGORIA, dbCon.Connection);

                cmd.Parameters.Add("categoria", MySqlDbType.Int32).Value = (int)categoria;

                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        listaRetorno.Add(ConstruirProduto(reader));
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

        public List<Produto> ListarProdutosPorNome(string nomeProduto)
        {
            var dbCon = DBConnection.Instance();
            List<Produto> listaRetorno = new List<Produto>();

            if (dbCon.IsConnect())
            {
                string query = String.Format(LISTAR_PRODUTO_POR_NOME, nomeProduto);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        listaRetorno.Add(ConstruirProduto(reader));
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

        public int? Inserir(Produto produto)
        {
            int? idProdutoInserido = null;
            var dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(INSERIR, dbCon.Connection);

                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@preco_custo", produto.PrecoCusto);
                cmd.Parameters.AddWithValue("@preco_venda", produto.PrecoVenda);
                cmd.Parameters.AddWithValue("@data_aquisicao", produto.DataAquisicao);

                cmd.ExecuteNonQuery();
                idProdutoInserido = (int)cmd.LastInsertedId;

                cmd.Dispose();
            }
            return idProdutoInserido;
        }

        public void Atualizar(Produto produto)
        {
            var dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(ATUALIZAR, dbCon.Connection);

                cmd.Parameters.AddWithValue("@id", produto.Id);
                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@preco_custo", produto.PrecoCusto);
                cmd.Parameters.AddWithValue("@preco_venda", produto.PrecoVenda);
                cmd.Parameters.AddWithValue("@data_aquisicao", produto.DataAquisicao);

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
