using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LoginUsuarioDAL : DBConnection
    {
        #region Constantes

        private const string LISTAR_USUARIO_POR_NOME = "SELECT * FROM cliente WHERE nome LIKE '%{0}%';";

        #endregion

        #region Metodos Publicos

        public List<Usuario> ListarUsuariosPorNome(string nomeUsuario)
        {
            var dbCon = DBConnection.Instance();
            List<Usuario> listaRetorno = new List<Usuario>();

            if (dbCon.IsConnect())
            {
                string query = String.Format(LISTAR_USUARIO_POR_NOME, nomeUsuario);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        listaRetorno.Add(ConstruirUsuario(reader));
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

        public void RegistrarUsuario(Usuario usuario)
        {

        }

        #endregion

        #region Metodos Privados

        private Usuario ConstruirUsuario(MySqlDataReader reader)
        {
            return new Usuario()
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Email = reader.GetString(2),
                IdEndereco = reader.GetInt32(3),
                DDDfixo = reader.GetString(4),
                TelefoneFixo = reader.GetString(5)
            };
        }

        #endregion
    }
}
