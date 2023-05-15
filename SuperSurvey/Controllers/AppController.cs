using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperSurvey.Models;
using SuperSurvey.Repository;
using SuperSurvey.Filters;
using System.IO;
using System.Data;
using Newtonsoft.Json;

namespace SuperSurvey.Controllers
{
    [UrlCopy]
    public class AppController : BaseController
    {

        public ActionResult DashBoard()
        {

            ////////            string json = System.IO.File.ReadAllText(Server.MapPath("~/UploadJSON/T1.json"));

            ////////       var Obj=     JsonConvert.DeserializeObject<List<SurveyDataModels>>(json);



            ////////          string  Qry = "INSERT INTO [tbl_SurveyData] " +
            //////// "([Row_ID] " +
            //////// ",[Party_ID] " +
            //////// ",[Survey_ID] " +
            //////// ",[State_ID] " +
            //////// ",[LokSabha_ID] " +
            //////// ",[VidhanSabha_ID]" +
            //////// ",[Word_ID]" +
            ////////" ,[Booth_ID]" +
            ////////" ,[Area_ID]" +
            ////////" ,[OtherArea] " +
            ////////" ,[GPSCoordinate] " +
            ////////" ,[SurveyDate] " +
            ////////" ,[User_ID] " +
            ////////" ,[CreatedDate],[Persone_ID],[Street]) VALUES";









            ////////            string Values = ",";
            ////////            foreach (var l in Obj)
            ////////            {
            ////////                Values = Values + ",(" + l.RowID +
            ////////                    "," + l.Party_ID +
            ////////                    "," + l.Survey_ID +
            ////////                    "," + l.State_ID +
            ////////                    "," + l.LokSabha_ID +
            ////////                    "," + l.VidhanSabha_ID +
            ////////                    "," + l.Word_ID +
            ////////                    "," + l.Booth_ID +
            ////////                    "," + l.Area_ID +
            ////////                    ",'" + l.OtherArea + "'" +
            ////////                     ",'" + l.GPSCoordinate + "'" +
            ////////                     ",'" + l.SurveyDate + "'" +
            ////////                     "," + l.UserID +
            ////////                     ", GetDate()," + l.PersonID + "   ,'" + l.Street + "')";



            ////////            }

            ////////            //var x = Values.Substring(Values.Trim().Length - 1);


            ////////            Qry = Qry + Values.Replace(",,", "");

            ////////            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);



            return View();
        }

        public ActionResult SurveyData()
        {
            return View(CommonFunction.GetSurveyData(1, 10000));
        }



        public ActionResult ClientDetails()
        {
            return View();
        }


        public JsonResult GetSurveyData(int pageIndex)
        {

            //Added to similate delay so that we see the loader working
            //Must be removed when moving to production

            return Json(CommonFunction.GetSurveyData(pageIndex, 10000), JsonRequestBehavior.AllowGet);
        }

        // GET: App
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SurveyReport()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PartyList(int? Type_ID)
        {

            PartyModels models = new PartyModels();
            models.PartyList = CommonFunction.GetPartyList(Convert.ToInt32(Type_ID));
            models.Type_ID = Convert.ToInt32(Type_ID);

            models.PanelTitle = Convert.ToInt32(Type_ID) == 1 ? "Party List" : "Person List";



            return View(models);
        }
        public ActionResult SurveyList()
        {
            SurveyMasterModels models = new SurveyMasterModels();
            models._SurveyList = CommonFunction.GetSurveyList(ORG_ID);
            return View(models);
        }
        public ActionResult CreateSurveyFor()
        {
            return View();

        }

        public ActionResult SurveyForList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateParty(int? id, int? Type_ID)
        {

            PartyModels models = new PartyModels();

            models.PanelTitle = Convert.ToInt32(Type_ID) == 1 ? "Create Party" : "Create Person";
            if (id != null)
            {
                models = CommonFunction.GetPartyDetails(Convert.ToInt32(id));
                models.Submit_Text = "Update";
                models.PanelTitle = Convert.ToInt32(Type_ID) == 1 ? "Update Party Details" : "Update Person Details";
            }
            models.Type_ID = Convert.ToInt32(Type_ID);
            models.ORG_ID = ORG_ID;



            return View(models);



        }
        [HttpPost]
        public ActionResult CreateParty(PartyModels models, HttpPostedFileBase IconsPath)
        {
            if (ModelState.IsValid)
            {

                models.ORG_ID = ORG_ID;

                if (models.Submit_Text.ToUpper() == "SAVE")
                {
                    string _ID = CommonFunction.GetRecordID("Select max(id) + 1 from tbl_PartyMaster ");
                    if (IconsPath != null && IconsPath.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(IconsPath.FileName);
                        string _path = Path.Combine(Server.MapPath("~/img"), _ID + "_IMG" + Path.GetExtension(IconsPath.FileName));
                        IconsPath.SaveAs(_path);
                        models.IconsPath = "http://api.kkminfotech.com/img/" + _ID + "_IMG" + Path.GetExtension(IconsPath.FileName);
                    }
                    else
                    {
                        TempData["Message"] = "Please Selet Image";
                        return View(models);
                    }
                }
                else
                {
                    if (IconsPath != null && IconsPath.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(IconsPath.FileName);
                        string _path = Path.Combine(Server.MapPath("~/img"), models.Party_ID + "_IMG" + Path.GetExtension(IconsPath.FileName));
                        IconsPath.SaveAs(_path);
                        models.IconsPath = "http://api.kkminfotech.com/img/" + models.Party_ID + "_IMG" + Path.GetExtension(IconsPath.FileName);
                    }

                }
                CommonFunction.SaveParty(models);
                TempData["Message"] = "Data submited successfully.";

                return RedirectToAction("PartyList", new { @Type_ID = models.Type_ID });
            }


            //  models.Type_ID = Convert.ToInt32(Type_ID);
            return View(models);



        }
        [HttpGet]
        public ActionResult CreateSurvey(int? id)
        {
            SurveyMasterModels models = new SurveyMasterModels();
            models.ORG_ID = ORG_ID;
            models.Submit_Text = "Save";
            models.Survey_For_List = CommonFunction.Survey_For_List();
            models.PartyList = CommonFunction.GetPartyList(1).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();
            models.PersoneList = CommonFunction.GetPartyList(2).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();
            models.PartyPersoneList = CommonFunction.GetPartyList(3).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();
            if (id != null)
            {
                models.SurveyID = Convert.ToInt32(id);
                DataSet ds = new DataSet();
                string _Qry = "Select * from tbl_SurveyNameMaster Where ID=" + Convert.ToInt32(id);
                _Qry = _Qry + " ;Select * from tbl_Survey_PartyMap Where Survey_ID=" + Convert.ToInt32(id);
                ds = CommonFunction.GetDataSet(_Qry);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    models.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    models.Description = ds.Tables[0].Rows[0]["Discreption"].ToString();
                    models.Survey_For_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Survey_For_ID"].ToString());
                    models.Survey_Type_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Survey_Type_ID"].ToString());
                    models.Submit_Text = "Update";

                    if (models.Survey_Type_ID == 1)
                    {


                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            foreach (var Mlist in models.PartyList.Where(m => m.Value == dr["Party_ID"].ToString()))
                            {

                                Mlist.Checked = true;
                            }
                        }
                        var xxx= models.PartyList.OrderByDescending(x => x.Checked).ThenBy(x1=>x1.Text).ToList<CheckListBoxModels>();
                        models.PartyList = xxx;
                    }
                    else if (models.Survey_Type_ID == 2)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            foreach (var Mlist in models.PersoneList.Where(m => m.Value == dr["Party_ID"].ToString()))
                            {

                                Mlist.Checked = true;
                            }

                        }

                        var xxx = models.PersoneList.OrderByDescending(x => x.Checked).ThenBy(x1 => x1.Text).ToList<CheckListBoxModels>();

                        models.PersoneList = xxx;
                    }
                    else if (models.Survey_Type_ID == 3)
                    {

                    }

                }
            }
            return View(models);
        }

        [HttpGet]
        public ActionResult EditSurvey(int? id)
        {
            SurveyMasterModels models = new SurveyMasterModels();
            if (id != null && Convert.ToInt32(id) > 0)
            {

                models = CommonFunction.GetSurveyDetails(Convert.ToInt32(id));
                models.SurveyID = Convert.ToInt32(id);
            }

            return View(models);
        }

        [HttpPost]
        public ActionResult EditSurvey(SurveyMasterModels models, FormCollection frm)
        {
            string PartyIDS = frm["PartyIDS"].ToString();
            string OrderIndex = frm["OrderIndex"].ToString();

            models.PartyIDS = PartyIDS;
            models.OrderIndex = OrderIndex;


            int a = CommonFunction.ChangePartyOrderInSurvey(models);

            TempData["Message"] = "Data Updated Successfully";

            return RedirectToAction("EditSurvey", new { @id = models.SurveyID });
        }


        [HttpPost]
        public ActionResult CreateSurvey(SurveyMasterModels models, FormCollection frm)
        {

            bool Flag = false;

            if (ModelState.IsValid)
            {
                Flag = true;
                if (models.Survey_Type_ID == 1)
                {
                    models.PartyIDS = string.Join(",", models.PartyList.Cast<CheckListBoxModels>().Where(n => n.Checked).Select(n => Convert.ToInt32(n.Value)).ToList());
                    if (string.IsNullOrEmpty(models.PartyIDS))
                    {
                        Flag = false;
                        TempData["Message"] = "Please Select Party";
                    }
                }
                else if (models.Survey_Type_ID == 2)
                {
                    models.PartyIDS = string.Join(",", models.PersoneList.Cast<CheckListBoxModels>().Where(n => n.Checked).Select(n => Convert.ToInt32(n.Value)).ToList());
                    if (string.IsNullOrEmpty(models.PartyIDS))
                    {
                        Flag = false;
                        TempData["Message"] = "Please Select Person";
                    }
                }
                else if (models.Survey_Type_ID == 3)
                {
                    models.PartyIDS = frm["PartyIDS"].ToString();
                    models.PersonIDS = frm["PersonIDS"].ToString();


                    models.PartyIDS = string.Join(",", frm["PartyIDS"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                    models.PersonIDS = string.Join(",", frm["PersonIDS"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));


                    if (models.PartyIDS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length == 0 || models.PersonIDS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length == 0)
                    {
                        Flag = false;
                        TempData["Message"] = "Please select Person and Party Name.";

                    }


                    else if (models.PartyIDS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length != models.PersonIDS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length)
                    {
                        Flag = false;
                        TempData["Message"] = "Please select Person and Party Name with same number.";

                    }
                    else if (models.PartyIDS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).GroupBy(x => x).Any(g => g.Count() > 1) == true || models.PersonIDS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).GroupBy(x => x).Any(g => g.Count() > 1) == true)
                    {
                        Flag = false;
                        TempData["Message"] = "Party Name or Person Name must be unique";
                    }




                }

                if (Flag == false)
                {
                    models.ORG_ID = ORG_ID;
                    models.Survey_For_List = CommonFunction.Survey_For_List();
                    models.PartyList = CommonFunction.GetPartyList(1).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();
                    models.PersoneList = CommonFunction.GetPartyList(2).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();
                    models.PartyPersoneList = CommonFunction.GetPartyList(3).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();
                    return View(models);
                }


                CommonFunction.SaveUpdateSurvey(models);
                TempData["Message"] = "Data Submited Successfully";
                return RedirectToAction("SurveyList");
            }
            models.Survey_For_List = CommonFunction.Survey_For_List();
            models.PartyList = CommonFunction.GetPartyList(1).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();
            models.PersoneList = CommonFunction.GetPartyList(2).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();
            models.PartyPersoneList = CommonFunction.GetPartyList(3).Select(x => new CheckListBoxModels { Text = x.Name, Value = x.Party_ID.ToString(), Type_ID = x.Type_ID }).ToList();

            return View(models);
        }
        [HttpGet]
        public ActionResult SurveyAssign(int? id)
        {
            SurveyAssingModels models = new SurveyAssingModels();
            models.ORG_ID = ORG_ID;
            models.SurveyList = CommonFunction.GetSurveyList(ORG_ID).Select(x => new SelectListItem { Text = x.Name, Value = x.SurveyID.ToString() }).ToList();
            models.StateList = CommonFunction.GetStateList();
            models.UserList = CommonFunction.GetUserListForSurveyAssign(ORG_ID);

            if (id != null && Convert.ToInt32(id) > 0)
            {
                models.SurveyID = Convert.ToInt32(id);
                models.UserList = CommonFunction.GetUserListForSurveyAssign(ORG_ID, models.SurveyID);
                var xx = models.UserList.OrderByDescending(x => x.IsChecked).ThenBy(x => x.User_Name).ToList<UserRegisterModels>();
                models.UserList = xx;



            }




            return View(models);
        }

        [HttpPost]
        public ActionResult SurveyAssign(SurveyAssingModels models)
        {



            models.UserIDS = string.Join(",", models.UserList.Cast<UserRegisterModels>().Where(n => n.IsChecked).Select(n => Convert.ToInt32(n.UserID)).ToList());

            if (string.IsNullOrEmpty(models.UserIDS))
            {
                TempData["Message"] = "Please select at least one User from user list ";
            }
            else
            {
                CommonFunction.SurveyAssign(models);
                TempData["Message"] = "Survey Assign Successfully";
                return RedirectToAction("SurveyList");

            }


            models.SurveyList = CommonFunction.GetSurveyList(ORG_ID).Select(x => new SelectListItem { Text = x.Name, Value = x.SurveyID.ToString() }).ToList();
            models.StateList = CommonFunction.GetStateList();
            models.UserList = CommonFunction.GetUserListForSurveyAssign(ORG_ID);
            TempData["Message"] = "Some thing wrong, Pleae try again.";
            return View(models);


        }


        public ActionResult UserList()
        {
            UserRegisterModels models = new UserRegisterModels();
            models._UserList = CommonFunction.GetUserList(ORG_ID);
            return View(models);
        }



        [HttpGet]
        public ActionResult CreateUser()
        {
            UserRegisterModels models = new UserRegisterModels();
            models.ORG_ID = ORG_ID;
            return View(models);
        }

        [HttpPost]
        public ActionResult CreateUser(UserRegisterModels models)
        {
            if (ModelState.IsValid)
            {
                CommonFunction.RegisterUser(models);
                TempData["Message"] = "Users Registerd Successfully. ";
                return RedirectToAction("UserList");
            }
            return View(models);
        }



        public ActionResult IconsList()
        {
            IconsModels models = new IconsModels();
            DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/img"));//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles(); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {

                models._List.Add(new IconsModels { ImageName = file.Name.Replace(file.Extension, ""), ImagePath = "http://api.kkminfotech.com/img/" + file.Name });
                str = str + ", " + file.Name;
            }




            return View(models);
        }


        [HttpGet]
        public ActionResult UploadIcons()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadIcons(HttpPostedFileBase file)
        {

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/img"), _FileName);
                    file.SaveAs(_path);
                }
                TempData["Message"] = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                TempData["Message"] = "File upload failed!!";
                return View();
            }
        }



    }
}