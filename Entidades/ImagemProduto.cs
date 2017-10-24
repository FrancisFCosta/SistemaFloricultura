using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ImagemProduto
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public String NomeImagem { get; set; }
        public byte[] ImagemBytes { get; set; }
        public bool IdcPrincipal { get; set; }
        public int TamanhoImagem{ get; set; }
    }
}
