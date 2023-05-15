using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperSurvey.Models;
using SuperSurvey.Repository;
namespace SuperSurvey.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult State()
        {
            StateModels model = new StateModels();

            model._List = CommonFunction.GetStateListForView();
            return View(model);

        }
        public ActionResult District()
        {
            DistrictModels models = new DistrictModels();
            return View(models);

        }
        public ActionResult Loksabha()
        {
            LoksabhaModels model = new LoksabhaModels();

            model._List = CommonFunction.GetLoksabhaListForView();
            return View(model);

        }
        public ActionResult Vidhansabha()
        {
            VidhansabhaModels model = new VidhansabhaModels();

            model._List = CommonFunction.GetVidhansabhaListForView();
            return View(model);

        }
        public ActionResult WardBlock()
        {
            WardBlockModels model = new WardBlockModels();

            model._List = CommonFunction.GetWardBlockListForView();
            return View(model);

        }
        public ActionResult City()
        {
            CityModels models = new CityModels();
            return View(models);

        }
        public ActionResult Booth()
        {
            BoothModels models = new BoothModels();
            return View(models);

        }


    }
}