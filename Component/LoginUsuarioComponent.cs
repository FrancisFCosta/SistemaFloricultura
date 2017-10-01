using Business;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    public class LoginUsuarioComponent
    {
        #region Propriedades
        private LoginUsuarioBLL loginUsuarioBll;
        #endregion

        #region Construtores
        public LoginUsuarioComponent()
        {
            loginUsuarioBll = new LoginUsuarioBLL();
        }
        #endregion

        #region Metodos Publicos

        public void RegistrarUsuario(Usuario usuario)
        {
            loginUsuarioBll.RegistrarUsuario(usuario);
        }

        public List<Usuario> ListarUsuariosPorNome(string nomeUsuario)
        {
            return loginUsuarioBll.ListarUsuariosPorNome(nomeUsuario);
        }
        #endregion
    }
}
