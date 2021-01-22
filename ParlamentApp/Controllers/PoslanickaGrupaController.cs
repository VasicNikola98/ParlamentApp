using ParlamentApp.Entities;
using ParlamentApp.Service;
using ParlamentApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParlamentApp.Controllers
{
    public class PoslanickaGrupaController : Controller
    {
        // GET: PoslanickaGrupa
        public ActionResult Index()
        {
            var model = new PoslanickaGrupaListingViewModel();
            model.Grupe = PoslanickaGrupaService.Instance.SveGrupe();
           
          
            return PartialView(model);
        }

        public ActionResult DodajGrupu()
        {
            var model = new PoslanickaGrupaViewModel();
            model.Poslanici = PoslanikService.Instance.SviPoslanici();
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult DodajGrupu(PoslanickaGrupa grupa)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (grupa != null)
            {
                PoslanickaGrupaService.Instance.DodajGrupu(grupa);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }

        public ActionResult DodajClanaUGrupu(string idGrupe)
        {
            var model = new PoslanickaGrupaViewModel2();
            model.Poslanici = PoslanikService.Instance.SviPoslanici();
            model.Poslanici2 = PoslanickaGrupaService.Instance.SviPoslaniciGrupe(idGrupe);
            model.GrupaId = idGrupe;
            foreach (var t in model.Poslanici2)
            {
                model.Poslanici = model.Poslanici.Where(x => x.Id != t.Id).ToList();
            }

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult DodajClanaUGrupu(string IdGrupe, string IdPoslanika)
        {

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            PoslanickaGrupaService.Instance.DodajClanaUGrupu(IdGrupe, IdPoslanika);
            result.Data = new { Success = true };

            return result;

        }

        [HttpPost]
        public JsonResult IzbaciClanaIzGrupe(string IdGrupe, string IdPoslanika)
        {

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            PoslanickaGrupaService.Instance.IzbaciClanaIzGrupe(IdGrupe, IdPoslanika);
            result.Data = new { Success = true };

            return result;

        }
        public ActionResult DodeliProstoriju(string IdGrupe, string IdPoslanika)
        {
            PoslanickaGrupaService.Instance.DodajClanaUGrupu(IdGrupe, IdPoslanika);
            return View();
        }
       
        
        public ActionResult IzmeniNazivGrupe(string IdGrupe)
        {
            var model = new PoslanickaGrupaEditViewModel();
            model.Grupa = PoslanickaGrupaService.Instance.VratiGrupu(IdGrupe);
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult IzmeniNazivGrupe(string IdGrupe, string NazivGrupe)
        {

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            PoslanickaGrupaService.Instance.IzmeniNazivGrupe(IdGrupe, NazivGrupe);
            result.Data = new { Success = true };

            return result;

        }

    }
}