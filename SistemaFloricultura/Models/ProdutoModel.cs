using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaFloricultura.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome Produto")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Preço de Custo")]
        public double PrecoCusto { get; set; }

        [Display(Name = "Preço de Venda")]
        public double PrecoVenda { get; set; }

        [Display(Name = "Data Aquisição")]
        public DateTime DataAquisicao { get; set; }

        public List<CategoriaProduto> LstCategorias { get; set; }

        public ImagemProduto ImagemPrincipal { get; set; }
    }
}