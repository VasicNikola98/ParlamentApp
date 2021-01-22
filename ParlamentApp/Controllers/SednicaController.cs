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
    public class SednicaController : Controller
    {
        // GET: Sednica
        public ActionResult Index() { 

            SednicaViewListingModel model = new SednicaViewListingModel();
            model.Sednice = SednicaService.Instance.SveSednice();
            return PartialView(model); 
        }

        public ActionResult PoslaniciSednice(string sednicaId)
        {
            SednicaService.Instance.VratiSvePrisutne(sednicaId);
            return PartialView();
        }

        public ActionResult DodajSednicu()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult DodajSednicu(Sednica sednica)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (sednica != null)
            {
                SednicaService.Instance.DodajSednicu(sednica);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }

        [HttpGet]
        [Route("PrisustvovaoJeSednici/{idPoslanika}")]
        public ActionResult PrisustvovaoJeSednici(string idPoslanika)
        {
            SednicaViewListingModel2 model = new SednicaViewListingModel2();
            model.Sednice = SednicaService.Instance.SveSednice();
            model.Sednice2 = SednicaService.Instance.SveSednice(idPoslanika);
            model.PoslanikId = idPoslanika;
            foreach(var t in model.Sednice2)
            {
                model.Sednice = model.Sednice.Where(x => x.Id != t.Id).ToList();
            }

            return PartialView(model);
        }

        [HttpPost]
        [Route("NijePrisustvovao/{idPoslanika}/{idSednice}")]
        public JsonResult NijePrisustvovao(string idPoslanika, string idSednice)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            SednicaService.Instance.NijePrisustvovao(idPoslanika, idSednice);
            result.Data = new { Success = true };

            return result;
        }

        public JsonResult PrisustvovaoJeSednici(string PoslanikId , string SednicaId)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
           
            SednicaService.Instance.PrisustvovaoJeSednici(PoslanikId,SednicaId);
            result.Data = new { Success = true };

            return result;
        }

        public JsonResult IzbrisiSednicu(string IdSednice)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            SednicaService.Instance.IzbrisiSednicu(IdSednice);
            result.Data = new { Success = true };

            return result;
        }
    }
}