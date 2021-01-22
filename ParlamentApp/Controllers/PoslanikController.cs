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
    public class PoslanikController : Controller
    {
        // GET: Poslanik
        public ActionResult Index()
        {
            PoslanikListingViewModel model = new PoslanikListingViewModel();
            model.Poslanici = PoslanikService.Instance.SviPoslanici();
            return PartialView(model);
        }

        public ActionResult DodajPoslanika()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult DodajPoslanika(Poslanik poslanik)
        {

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (poslanik != null)
            {
                PoslanikService.Instance.DodajPoslanika(poslanik);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }
    }
}