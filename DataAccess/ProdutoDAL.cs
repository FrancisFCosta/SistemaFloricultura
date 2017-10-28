﻿using Entidades;
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

        private const string LISTAR_PRODUTOS = "SELECT * FROM produto"; 
        private const string LISTAR_PRODUTO_POR_NOME = "SELECT * FROM produto WHERE nome_produto LIKE '%{0}%';";
        #endregion

        #region Metodos Publicos

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
