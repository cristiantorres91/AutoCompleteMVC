using PruebaAutocomplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaAutocomplete.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetDepartamento(string departamento)
        {
            PaisEntities bd = new PaisEntities();
            //cargo datos sin filtro
            var dataList = bd.Departamento.ToList();
            //si se escribe se filtra dato
            if(departamento != null)
            {
                //busco dato filtrado
                 dataList = bd.Departamento.Where(x => x.Departamento1.Contains(departamento)).ToList();
            }

            //datos a usar
            var modificarData = dataList.Select(x => new
            {
                id= x.Id,
                text=x.Departamento1
            });
            //retorno datos como json
            return Json(modificarData, JsonRequestBehavior.AllowGet);
        }

        //Metodo donde obtengo dato
        [HttpPost]
        public ActionResult Save(string id)
        {
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}