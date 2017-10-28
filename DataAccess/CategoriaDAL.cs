using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoriaDAL : DBConnection
    {
        #region Constantes

        private const string INSERIR = @"
            INSERT INTO Categoria 
            (
                `produto_id`,
                `categoria`
            ) 
            VALUES 
            (
                @produto_id, 
                @categoria
            );";

        private const string LISTAR_CATEGORIA_POR_ID = "SELECT * FROM Categoria WHERE produto_id = @produto_id;";
        private const string EXCLUIR_POR_ID_PRODUTO = "DELETE FROM `categoria` WHERE `produto_id` = @produto_id;";

        #endregion

        #region Metodos Publicos

        public List<Categoria> ListarCategoriasPorId(int idProduto)
        {
            var dbCon = DBConnection.Instance();
            List<Categoria> listaRetorno = new List<Categoria>();

            if (dbCon.IsConnect())
            {
                string query = String.Format(LISTAR_CATEGORIA_POR_ID, idProduto);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        listaRetorno.Add(ConstruirCategoria(reader));
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

        public void Inserir(Categoria Categoria)
        {
            var dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(INSERIR, dbCon.Connection);

                cmd.Parameters.AddWithValue("@produto_id", Categoria.IdProduto);
                cmd.Parameters.AddWithValue("@categoria", Categoria.CodigoCategoria);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public void ExcluirPorId(int idProduto)
        {
            var dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(EXCLUIR_POR_ID_PRODUTO, dbCon.Connection);

                cmd.Parameters.AddWithValue("@produto_id", idProduto);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        #endregion

        #region Metodos Privados

        private Categoria ConstruirCategoria(MySqlDataReader reader)
        {
            return new Categoria()
            {
                IdProduto = reader.GetInt32(0),
                CodigoCategoria = (CategoriaProduto)reader.GetInt32(1)
            };
        }

        #endregion
    }
}
