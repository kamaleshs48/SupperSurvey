using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperSurvey.Models;
using SuperSurvey.Repository;

namespace SuperSurvey.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index1()
        {

            string json = System.IO.File.ReadAllText(Server.MapPath("~/UploadJSON/T1.json"));

            // JsonConvert.DeserializeObject<List<RetrieveMultipleResponse>>(JsonStr);


            var Obj123 = JsonConvert.DeserializeObject<dynamic>(json);


            string xxxxx =Convert.ToString( Obj123[0]);

            string xxxxx1 = Convert.ToString(Obj123[1]);


            var Obj = JsonConvert.DeserializeObject<List<SurveyDataModels>>(xxxxx);


            var xxx = CommonFunction.ListToDataTable(Obj);

            string Qry = "INSERT INTO [tbl_SurveyData] " +
           "([Row_ID] " +
           ",[Party_ID] " +
           ",[Survey_ID] " +
           ",[State_ID] " +
           ",[LokSabha_ID] " +
           ",[VidhanSabha_ID]" +
           ",[Word_ID]" +
          " ,[Booth_ID]" +
          " ,[Area_ID]" +
          " ,[OtherArea] " +
          " ,[GPSCoordinate] " +
          " ,[SurveyDate] " +
          " ,[User_ID] " +
          " ,[CreatedDate]) VALUES";

            string Values = ",";
            foreach (var l in Obj)
            {
                Values = Values + ",(" + l.RowID +
                    "," + l.Party_ID +
                    "," + l.Survey_ID +
                    "," + l.State_ID +
                    "," + l.LokSabha_ID +
                    "," + l.VidhanSabha_ID +
                    "," + l.Word_ID +
                    "," + l.Booth_ID +
                    "," + l.Area_ID +
                    ",'" + l.OtherArea + "'" +
                     ",'" + l.GPSCoordinate + "'" +
                     ",'" + l.SurveyDate + "'" +
                     "," + l.UserID +
                     ", GetDate())";



            }

            var x = Values.Substring(Values.Trim().Length - 1);


            string q = Qry + Values.Replace(",,", "");




            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";

          //  CommonFunction.SendSMS("7042771792", "Heloo");
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModels models)
        {

            if (models.EmailID == "admin@gmail.com" && models.Password == "123456")
            {
                Session["UserID"] = 1;
                return RedirectToAction("DashBoard","App");
            }
            return View();

        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SurveyData()
        {
            return View();
        }

        public ActionResult MemberProfile()
        {
            return View();
        }




        public ActionResult ClearData()
        {
            CommonFunction.ClearSurveyData();
            return View();
        }
    }
}