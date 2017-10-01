using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFloricultura.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public DateTime DataAquisicao { get; set; }
    }
}