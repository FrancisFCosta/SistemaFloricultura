using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ImagemProdutoDAL : DBConnection
    {
        #region Constantes

        private const string INSERIR = @"
            INSERT INTO imagem_produto 
            (
                `produto_id`,
                `imagem`, 
                `idc_principal`, 
                `nomeImagem`,
                `tamanho_imagem`
            ) 
            VALUES 
            (
                @produto_id, 
                @imagem, 
                @idc_principal, 
                @nomeImagem,
                @tamanho_imagem
            );";

        private const string OBTER_IMAGEM_PRODUTO = "SELECT * FROM ImagemProduto WHERE produto_id = '%{0}%';";

        #endregion

        #region Metodos Publicos

        public List<ImagemProduto> ListarImagemProdutosPorNome(string nomeImagemProduto)
        {
            var dbCon = DBConnection.Instance();
            List<ImagemProduto> listaRetorno = new List<ImagemProduto>();

            if (dbCon.IsConnect())
            {
                string query = String.Format(OBTER_IMAGEM_PRODUTO, nomeImagemProduto);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        listaRetorno.Add(ConstruirImagemProduto(reader));
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

        public void Inserir(ImagemProduto ImagemProduto)
        {
            var dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(INSERIR, dbCon.Connection);
                
                MySqlParameter produtoIdParameter = new MySqlParameter("@produto_id", MySqlDbType.Int32, 11);
                MySqlParameter nomeImagemParameter = new MySqlParameter("@nomeImagem", MySqlDbType.VarChar, 256);
                MySqlParameter idcPrincipalParameter = new MySqlParameter("@idc_principal", MySqlDbType.Bit, 1);
                MySqlParameter imagemContentParameter = new MySqlParameter("@imagem", MySqlDbType.Blob, ImagemProduto.ImagemBytes.Length);
                MySqlParameter tamanhoParameter = new MySqlParameter("@tamanho_imagem", MySqlDbType.Int32, 11);

                produtoIdParameter.Value = ImagemProduto.IdProduto;
                nomeImagemParameter.Value = ImagemProduto.NomeImagem;
                idcPrincipalParameter.Value = ImagemProduto.IdcPrincipal;
                imagemContentParameter.Value = ImagemProduto.ImagemBytes;
                tamanhoParameter.Value = ImagemProduto.TamanhoImagem;
                                
                cmd.Parameters.Add(produtoIdParameter);
                cmd.Parameters.Add(nomeImagemParameter);
                cmd.Parameters.Add(idcPrincipalParameter);
                cmd.Parameters.Add(imagemContentParameter);
                cmd.Parameters.Add(tamanhoParameter);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        #endregion

        #region Metodos Privados

        private ImagemProduto ConstruirImagemProduto(MySqlDataReader reader)
        {
            return new ImagemProduto()
            {
                Id = reader.GetInt32(0),
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
