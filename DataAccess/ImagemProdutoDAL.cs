using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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

        private const string OBTER_IMAGEM_PRODUTO = @"SELECT id, produto_id, imagem, idc_principal, nomeImagem, tamanho_imagem FROM imagem_produto WHERE produto_id = {0};";

        #endregion

        #region Metodos Publicos

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

        public ImagemProduto ObterImagemPrincipal(int idProduto)
        {
            var dbCon = DBConnection.Instance();
            ImagemProduto imagemRecuperada = null;

            if (dbCon.IsConnect())
            {
                string query = String.Format(OBTER_IMAGEM_PRODUTO, idProduto);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        return ConstruirImagemProduto(reader);
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

            return imagemRecuperada;

        }

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

        #endregion

        #region Metodos Privados

        private ImagemProduto ConstruirImagemProduto(MySqlDataReader reader)
        {
            return new ImagemProduto()
            {
                Id = reader.GetInt32(0),
                IdProduto = reader.GetInt32(1),
                ImagemBytes = (Byte[])(reader["imagem"]),
                ImagemBitMap = ByteToImage((Byte[])(reader["imagem"])),
                IdcPrincipal = reader.GetInt32(3) > 0,
                NomeImagem = reader.GetString(4),
                TamanhoImagem = reader.GetInt32(5)
            };
        }

        private Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        #endregion
    }
}
