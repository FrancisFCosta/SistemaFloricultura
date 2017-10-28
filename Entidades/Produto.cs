using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public DateTime DataAquisicao { get; set; }
        public ImagemProduto ImagemPrincipal { get; set; }
        public List<CategoriaProduto> ListaCategorias { get; set; }
    }
}
