using DataAccess;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class LoginUsuarioBLL
    {
        #region Propriedades
        private LoginUsuarioDAL loginUsuarioDAL;
        #endregion

        #region Construtores
        public LoginUsuarioBLL()
        {
            loginUsuarioDAL = new LoginUsuarioDAL();
        }
        #endregion

        #region Metodos Publicos

        public void RegistrarUsuario(Usuario usuario)
        {
            loginUsuarioDAL.RegistrarUsuario(usuario);
        }

        public List<Usuario> ListarUsuariosPorNome(string nomeUsuario)
        {
            return loginUsuarioDAL.ListarUsuariosPorNome(nomeUsuario);
        }
        #endregion
    }
}
