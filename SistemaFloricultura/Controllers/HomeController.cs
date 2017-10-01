using Component;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFloricultura.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LoginUsuarioComponent usuariocomponent = new LoginUsuarioComponent();
            List<Usuario> listaUsuarios = usuariocomponent.ListarUsuariosPorNome("francis");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}