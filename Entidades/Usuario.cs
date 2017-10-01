using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        #region Propriedades

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int IdEndereco { get; set; }
        public string DDDfixo { get; set; }
        public string TelefoneFixo { get; set; }

        #endregion

        #region Construtores

        public Usuario() { }

        #endregion
    }
}
