using Microsoft.AspNetCore.Mvc;
using MVCCoreEnlacesLink.Models;
using NetCoreLinqToSql.Repositories;

namespace NetCoreLinqToSql.Controllers
{
    public class EnfermosController : Controller
    {
        private RepositoryEnfermos repo;

        public EnfermosController()
        {
            this.repo = new RepositoryEnfermos();
        }

        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult Delete(int id)
        {
            this.repo.DeleteEnfermo(id);
            return RedirectToAction("Index");
        }

        public IActionResult BuscadorEnfermos()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        [HttpPost]
        public IActionResult BuscadorEnfermos(DateTime fecha)
        {
            List<Enfermo> enfermos =
                this.repo.BuscarEnfermo(fecha);
            if (enfermos == null)
            {
                ViewData["MENSAJE"] = "No existen enfermos con la fecha de nacimiento: "
                    + fecha;
                return View();
            }
            else
            {
                return View(enfermos);
            }
        }
    }
}
