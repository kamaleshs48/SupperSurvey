using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using SuperSurvey.Models;
using SuperSurvey.Repository;
using System.Web.Helpers;
using SuperSurvey.Filters;

namespace SuperSurvey.Controllers
{
    [UrlCopy]
    public class ReportController : BaseController
    {
        // GET: Report

        List<CheckListBoxModels> _list = new List<CheckListBoxModels>()
            {


                new CheckListBoxModels { Text="State",Value="SM.StateName",Order_Index="SM.StateName 'State'"},
                new CheckListBoxModels { Text="LokSabha",Value="LM.Name",Order_Index="LM.Name 'LokSabha'"},
                new CheckListBoxModels { Text="VidhanSabha",Value="VM.NAME",Order_Index="VM.NAME 'VidhanSabha'"},
                new CheckListBoxModels { Text="Ward",Value="WM.Name",Order_Index="WM.Name 'Ward'"},
                //new CheckListBoxModels { Text="Area",Value="CV.Name",Order_Index="CV.Name 'Area'"},
                new CheckListBoxModels { Text="Area",Value="SD.OtherArea",Order_Index="SD.OtherArea Area"},
                 new CheckListBoxModels { Text="Booth",Value="BM.Name",Order_Index="BM.Name Booth"},
                new CheckListBoxModels { Text="Street",Value="SD.Street",Order_Index="SD.Street Street"},
                new CheckListBoxModels { Text="Party/Person",Value="PM.Name",Order_Index="PM.Name 'Party/Person'"},
                 //new CheckListBoxModels { Text="Person",Value="PM1.Name",Order_Index="PM1.Name 'Person'"},
               // new CheckListBoxModels { Text="Survey Date",Value="SD.SurveyDate",Order_Index="SD.SurveyDate"},

            };

        public ActionResult SurveyResult()
        {

            NameCountModels models = new NameCountModels();



            // Bind Party




            //  models._List = CommonFunction.GetSurveyReport1(16);

            ViewBag.List = CommonFunction.GetSurveyReport2(16);






            return View(models);
        }


        [HttpGet]
        public ActionResult ReportFinel()
        {
            SurveyFinelReport model = new SurveyFinelReport();




            model.SurveyList = CommonFunction.GetSurveyList(ORG_ID).Select(n =>
                                                   new SelectListItem
                                                   {
                                                       Value = n.SurveyID.ToString(),
                                                       Text = n.Name
                                                   }).OrderBy(x => x.Text).ToList();



            model.GroupList = _list;


            return View(model);
        }

        [HttpPost]
        public ActionResult ReportFinel(SurveyFinelReport models)
        {



            DataSet ds = new DataSet();
            models.SurveyList = CommonFunction.GetSurveyList(ORG_ID).Select(n =>
                                                 new SelectListItem
                                                 {
                                                     Value = n.SurveyID.ToString(),
                                                     Text = n.Name
                                                 }).OrderBy(x => x.Text).ToList();



            int Type_ID = Convert.ToInt32(CommonFunction.GetRecordID("Select Survey_Type_ID from tbl_SurveyNameMaster Where ID= " + models.SurveyID));


            if (Type_ID == 1 || Type_ID == 2)
            {



                string _Fileds = string.Join(",", models.GroupList.Where(x => x.Checked).Select(x => x.Value).ToArray());
                string _SFileds = string.Join(",", models.GroupList.Where(x => x.Checked).Select(x => x.Order_Index).ToArray());


                if (_SFileds == "" || _SFileds.Split(',').Length < 2)
                {
                    TempData["Message"] = "Please select atleast two fields";
                    models.GroupList = _list;
                    return View(models);
                }
                //SUM(COUNT(SD.ID)) OVER() AS total_count





                string _Fld = "Select  row_number() over(order by " + _Fileds + ") as 'SR.N.'," + _SFileds + @", count(SD.ID) Total   into #xx from tbl_SurveyData SD   
INNER Join tbl_UserMaster UM On Um.ID=SD.User_ID  
INNER JOIN tbl_SurveyNameMaster SNM ON SNM.ID=SD.Survey_ID  
LEFT Join tbl_PartyMaster PM ON PM.ID=SD.Party_ID 

INNER Join tbl_StateMaster SM ON SM.ID=SD.State_ID  
LEFT JOIN tbl_Loksabha LM ON LM.ID=SD.LokSabha_ID  
LEFT JOIN  tbl_Vidhansabha VM ON VM.ID=SD.VidhanSabha_ID  
LEFT JOIN tbl_WordBlock WM  ON WM.ID=SD.Word_ID 
LEFT JOIN tbl_BoothMaster BM ON BM.ID=SD.Booth_ID 
LEFT JOIN tbl_AreaCityVillage CV on CV.ID=SD.Area_ID  Where SD.Survey_ID = " + models.SurveyID;


                if (!string.IsNullOrEmpty(models.FDate) && !string.IsNullOrEmpty(models.TDate))
                {
                    _Fld = _Fld + " AND SD.CreatedDate BETWEEN convert(datetime,'" + models.FDate + "',103) AND  convert(datetime,'" + models.TDate + "',103)";
                }
                if (!string.IsNullOrEmpty(models.SerachFieldsName) && !string.IsNullOrEmpty(models.SerachFieldsValue))
                {
                    _Fld = _Fld + " AND " + models.SerachFieldsName + " Like '%" + models.SerachFieldsValue + "%'";
                }
                _Fld = _Fld + " Group by  " + _Fileds + " Order by " + _Fileds + "";
                //Select Count(ID) FROM tbl_SurveyData WHERE Survey_ID = " + models.SurveyID
                string Qry1 = _Fld;

                _Fld = "Select  row_number() over(order by " + _Fileds.Remove(_Fileds.LastIndexOf(',')) + ") as 'SR.N.'," + _SFileds.Remove(_SFileds.LastIndexOf(',')) + @", count(SD.ID) Total Into #xx1 from tbl_SurveyData SD   
INNER Join tbl_UserMaster UM On Um.ID=SD.User_ID  
INNER JOIN tbl_SurveyNameMaster SNM ON SNM.ID=SD.Survey_ID  
LEFT Join tbl_PartyMaster PM ON PM.ID=SD.Party_ID 

INNER Join tbl_StateMaster SM ON SM.ID=SD.State_ID  
LEFT JOIN tbl_Loksabha LM ON LM.ID=SD.LokSabha_ID  
LEFT JOIN  tbl_Vidhansabha VM ON VM.ID=SD.VidhanSabha_ID  
LEFT JOIN tbl_WordBlock WM  ON WM.ID=SD.Word_ID 
LEFT JOIN tbl_BoothMaster BM ON BM.ID=SD.Booth_ID 
LEFT JOIN tbl_AreaCityVillage CV on CV.ID=SD.Area_ID  Where SD.Survey_ID = " + models.SurveyID;


                if (!string.IsNullOrEmpty(models.FDate) && !string.IsNullOrEmpty(models.TDate))
                {
                    _Fld = _Fld + " AND SD.CreatedDate BETWEEN convert(datetime,'" + models.FDate + "',103) AND  convert(datetime,'" + models.TDate + "',103)";
                }
                if (!string.IsNullOrEmpty(models.SerachFieldsName) && !string.IsNullOrEmpty(models.SerachFieldsValue))
                {
                    _Fld = _Fld + " AND " + models.SerachFieldsName + " Like '%" + models.SerachFieldsValue + "%'";
                }
                _Fld = _Fld + " Group by  " + _Fileds.Remove(_Fileds.LastIndexOf(',')) + " Order by " + _Fileds.Remove(_Fileds.LastIndexOf(',')) + "";


                Qry1 = Qry1 + " " + _Fld;




                string QQ = Qry1 + @"; select x1.*,CAST( (CAST((100*x1.Total) as numeric(10,2))/cast(x.Total as numeric(10,2))) as numeric(10,2)) 'Percentage'  from  #xx1 x inner join #xx  x1
ON x." + _SFileds.Split(',')[_SFileds.Split(',').Length - 2].Split(' ')[1].Replace("'", "") + " = x1." + _SFileds.Split(',')[_SFileds.Split(',').Length - 2].Split(' ')[1].Replace("'", "") + " " + @"--Select * from #xx1
--select * from #xx



drop table #xx1
drop table #xx";
                QQ = QQ + "  Select Count(ID) FROM tbl_SurveyData WHERE Survey_ID = " + models.SurveyID;



                ds = CommonFunction.GetDataSet(QQ);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    models.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                }
                ViewBag.Sum = "0";
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Sum = ds.Tables[0].Compute("Sum(Total)", string.Empty);
                }

            }
            else if (Type_ID == 3)

            {



                string _Fileds = string.Join(",", models.GroupList.Where(x => x.Checked).Select(x => x.Value).ToArray());
                string _SFileds = string.Join(",", models.GroupList.Where(x => x.Checked).Select(x => x.Order_Index).ToArray());
                _Fileds = _Fileds.Replace("PM.Name", "PM.Name,PM1.Name");
                _SFileds = _SFileds.Replace("PM.Name 'Party/Person'", "PM.Name 'Party',PM1.Name 'Person'");




                if (_SFileds == "" || _SFileds.Split(',').Length < 2)
                {
                    TempData["Message"] = "Please select atleast two fields";
                    models.GroupList = _list;
                    return View(models);
                }
                //SUM(COUNT(SD.ID)) OVER() AS total_count





                string _Fld = "Select  row_number() over(order by " + _Fileds + ") as 'SR.N.'," + _SFileds + @", count(SD.ID) Total   into #xx from tbl_SurveyData SD   
INNER Join tbl_UserMaster UM On Um.ID=SD.User_ID  
INNER JOIN tbl_SurveyNameMaster SNM ON SNM.ID=SD.Survey_ID  
LEFT Join tbl_PartyMaster PM ON PM.ID=SD.Party_ID 
LEFT Join tbl_PartyMaster PM1 ON PM1.ID=SD.Persone_ID 
INNER Join tbl_StateMaster SM ON SM.ID=SD.State_ID  
LEFT JOIN tbl_Loksabha LM ON LM.ID=SD.LokSabha_ID  
LEFT JOIN  tbl_Vidhansabha VM ON VM.ID=SD.VidhanSabha_ID  
LEFT JOIN tbl_WordBlock WM  ON WM.ID=SD.Word_ID 
LEFT JOIN tbl_BoothMaster BM ON BM.ID=SD.Booth_ID 
LEFT JOIN tbl_AreaCityVillage CV on CV.ID=SD.Area_ID  Where SD.Survey_ID = " + models.SurveyID;


                if (!string.IsNullOrEmpty(models.FDate) && !string.IsNullOrEmpty(models.TDate))
                {
                    _Fld = _Fld + " AND SD.CreatedDate BETWEEN convert(datetime,'" + models.FDate + "',103) AND  convert(datetime,'" + models.TDate + "',103)";
                }
                if (!string.IsNullOrEmpty(models.SerachFieldsName) && !string.IsNullOrEmpty(models.SerachFieldsValue))
                {
                    _Fld = _Fld + " AND " + models.SerachFieldsName + " Like '%" + models.SerachFieldsValue + "%'";
                }
                _Fld = _Fld + " Group by  " + _Fileds + " Order by " + _Fileds + "";
                //Select Count(ID) FROM tbl_SurveyData WHERE Survey_ID = " + models.SurveyID
                string Qry1 = _Fld;

                _Fld = "Select  row_number() over(order by " + _Fileds.Remove(_Fileds.LastIndexOf(',')) + ") as 'SR.N.'," + _SFileds.Remove(_SFileds.LastIndexOf(',')) + @", count(SD.ID) Total Into #xx1 from tbl_SurveyData SD   
INNER Join tbl_UserMaster UM On Um.ID=SD.User_ID  
INNER JOIN tbl_SurveyNameMaster SNM ON SNM.ID=SD.Survey_ID  
LEFT Join tbl_PartyMaster PM ON PM.ID=SD.Party_ID 

INNER Join tbl_StateMaster SM ON SM.ID=SD.State_ID  
LEFT JOIN tbl_Loksabha LM ON LM.ID=SD.LokSabha_ID  
LEFT JOIN  tbl_Vidhansabha VM ON VM.ID=SD.VidhanSabha_ID  
LEFT JOIN tbl_WordBlock WM  ON WM.ID=SD.Word_ID 
LEFT JOIN tbl_BoothMaster BM ON BM.ID=SD.Booth_ID 
LEFT JOIN tbl_AreaCityVillage CV on CV.ID=SD.Area_ID  Where SD.Survey_ID = " + models.SurveyID;


                if (!string.IsNullOrEmpty(models.FDate) && !string.IsNullOrEmpty(models.TDate))
                {
                    _Fld = _Fld + " AND SD.CreatedDate BETWEEN convert(datetime,'" + models.FDate + "',103) AND  convert(datetime,'" + models.TDate + "',103)";
                }
                if (!string.IsNullOrEmpty(models.SerachFieldsName) && !string.IsNullOrEmpty(models.SerachFieldsValue))
                {
                    _Fld = _Fld + " AND " + models.SerachFieldsName + " Like '%" + models.SerachFieldsValue + "%'";
                }
                _Fld = _Fld + " Group by  " + _Fileds.Remove(_Fileds.LastIndexOf(',')) + " Order by " + _Fileds.Remove(_Fileds.LastIndexOf(',')) + "";


                Qry1 = Qry1 + " " + _Fld;




                string QQ = Qry1 + @"; select x1.*,CAST( (CAST((100*x1.Total) as numeric(10,2))/cast(x.Total as numeric(10,2))) as numeric(10,2)) 'Percentage'  from  #xx1 x inner join #xx  x1
ON x." + _SFileds.Split(',')[_SFileds.Split(',').Length - 2].Split(' ')[1].Replace("'", "") + " = x1." + _SFileds.Split(',')[_SFileds.Split(',').Length - 2].Split(' ')[1].Replace("'", "") + " " + @"--Select * from #xx1
--select * from #xx



drop table #xx1
drop table #xx";
                QQ = QQ + "  Select Count(ID) FROM tbl_SurveyData WHERE Survey_ID = " + models.SurveyID;



                ds = CommonFunction.GetDataSet(QQ);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    models.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                }
                ViewBag.Sum = "0";
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Sum = ds.Tables[0].Compute("Sum(Total)", string.Empty);
                }

            }

            DataTable dt = ds.Tables[0];
            List<WebGridColumn> columns = new List<WebGridColumn>();
            foreach (DataColumn col in dt.Columns)
            {
                columns.Add(new WebGridColumn()
                {
                    ColumnName = col.ColumnName,
                    Header = col.ColumnName
                });
            }
            //columns.Add(new WebGridColumn()
            //{
            //    Format = (item) =>
            //    {
            //        return new HtmlString(string.Format("<a href= {0}>Edit</a>", Url.Action("Edit", "Home", new
            //        {
            //            Id = item.SSN
            //        })));
            //    }
            //});
            ViewBag.Columns = columns;
            //Converting datatable to dynamic list     
            var dns = new List<dynamic>();
            dns = CommonFunction.ConvertDtToList(dt);
            ViewBag.Total = dns;

            models.GroupList = _list;
            models.SurveyList = CommonFunction.GetSurveyList(ORG_ID).Select(n =>
                                                 new SelectListItem
                                                 {
                                                     Value = n.SurveyID.ToString(),
                                                     Text = n.Name
                                                 }).OrderBy(x => x.Text).ToList();

            return View(models);
        }

        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            NameCountModels models = new NameCountModels();
            string Qry = @"Select SNM.ID,  SNM.Name ,count(SD.ID) TCount
            from tbl_SurveyData SD  INNER JOIN tbl_SurveyNameMaster SNM ON SNM.ID=SD.Survey_ID  
            GROUP BY SNM.ID,SNM.Name";
            ds = CommonFunction.GetDataSet(Qry);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    models._List.Add(new NameCountModels
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                        Count = dr["TCount"].ToString(),
                    });
                }
            }
            return View(models);
        }


        public ActionResult PartyWise(string id, string PName)
        {
            DataSet ds = new DataSet();
            ViewBag.PName = PName;
            NameCountModels models = new NameCountModels();
            if (!string.IsNullOrEmpty(id) && Convert.ToInt32(id) > 0)
            {

                string Qry = @"Select SNM.ID,  SNM.Name ,count(SD.ID) TCount
             from tbl_SurveyData SD  INNER JOIN tbl_PartyMaster SNM ON SNM.ID=SD.Party_ID " +
                 " Where Survey_ID = " + id + "GROUP BY SNM.ID,SNM.Name";
                ds = CommonFunction.GetDataSet(Qry);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        models._List.Add(new NameCountModels
                        {
                            ID = Convert.ToInt32(dr["ID"].ToString()),
                            Name = dr["Name"].ToString(),
                            Count = dr["TCount"].ToString(),
                            PID = Convert.ToInt32(id),

                            PName = PName,
                        });
                    }
                }
            }
            return View(models);
        }


        public ActionResult FullDetails(string id, string PID)
        {

            List<ReportDataModels> list = new List<ReportDataModels>();

            DataSet ds = new DataSet();


            if (!string.IsNullOrEmpty(id) && Convert.ToInt32(id) > 0)
            {

                string Qry = @"Select UM.User_Name, 
SNM.Name 'SurveyName', 
PM.Name 'PartyName',
SM.StateName,LM.Name 'LokSabhaName'  
,VM.NAME 'Vidhansabha_Name'  
,WM.Name 'Ward_Block'  
,CV.Name 'Area'  
,SD.OtherArea  
,SD.GPSCoordinate  
,SD.SurveyDate 'Survey_Date'  
,SD.CreatedDate 'Post_Date'  
 from tbl_SurveyData SD   
INNER Join tbl_UserMaster UM On Um.ID=SD.User_ID  
INNER JOIN tbl_SurveyNameMaster SNM ON SNM.ID=SD.Survey_ID  
INNER Join tbl_PartyMaster PM ON PM.ID=SD.Party_ID  
INNER Join tbl_StateMaster SM ON SM.ID=SD.State_ID  
LEFT JOIN tbl_Loksabha LM ON LM.ID=SD.LokSabha_ID  
LEFT JOIN  tbl_Vidhansabha VM ON VM.ID=SD.VidhanSabha_ID  
LEFT JOIN tbl_WordBlock WM  ON WM.ID=SD.Word_ID  
LEFT JOIN tbl_AreaCityVillage CV on CV.ID=SD.Area_ID WHERE SD.Party_ID=" + id + " AND SD.Survey_ID = " + PID;
                ds = CommonFunction.GetDataSet(Qry);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ViewBag.SurveyName = dr["SurveyName"].ToString();
                        ViewBag.PartyName = dr["PartyName"].ToString();

                        list.Add(new ReportDataModels
                        {

                            User_Name = dr["User_Name"].ToString(),
                            LokSabhaName = dr["LokSabhaName"].ToString(),
                            Vidhansabha_Name = dr["Vidhansabha_Name"].ToString(),
                            Ward_Block = dr["Ward_Block"].ToString(),
                            Area = dr["Area"].ToString(),
                            OtherArea = dr["OtherArea"].ToString(),
                            GPSCoordinate = dr["GPSCoordinate"].ToString(),
                            Survey_Date = dr["Survey_Date"].ToString(),
                            Post_Date = dr["Post_Date"].ToString(),


                        });
                    }
                }
            }
            return View(list);
        }


        [HttpGet]
        public ActionResult QuestionReport()
        {

            QuestionReportModels models = new QuestionReportModels();

            models.SurveyList = CommonFunction.GetSurveyList(ORG_ID).Select(n =>
                                                 new SelectListItem
                                                 {
                                                     Value = n.SurveyID.ToString(),
                                                     Text = n.Name
                                                 }).OrderBy(x => x.Text).ToList();


            models.QuestionListForSelection.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0",
            });

            return View(models);
        }


        [HttpPost]

        public ActionResult QuestionReport(QuestionReportModels models)
        {



            models.SurveyList = CommonFunction.GetSurveyList(ORG_ID).Select(n =>
                                                 new SelectListItem
                                                 {
                                                     Value = n.SurveyID.ToString(),
                                                     Text = n.Name
                                                 }).OrderBy(x => x.Text).ToList();







            DataSet ds = CommonFunction.GetQuestionReportDS(models.SurveyID);








            List<QuestionResponseModel> _OptionList = new List<QuestionResponseModel>();
            models.QuestionListForSelection.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0",
            });

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    _OptionList = new List<QuestionResponseModel>();

                    for (int j = 1; j < 31; j++)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Option" + j.ToString()].ToString()) && ds.Tables[0].Rows[i]["QType"].ToString() == "Single Choice")
                        {
                            _OptionList.Add(new QuestionResponseModel
                            {
                                OptionText = ds.Tables[0].Rows[i]["Option" + j.ToString()].ToString(),
                                OptionCount = GetResponseCountForSingleChoice(ds, i + 1, 0, j.ToString())
                            });

                        }

                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Option" + j.ToString()].ToString()) && ds.Tables[0].Rows[i]["QType"].ToString() == "Multiple Choice")
                        {
                            _OptionList.Add(new QuestionResponseModel
                            {
                                OptionText = ds.Tables[0].Rows[i]["Option" + j.ToString()].ToString(),
                                OptionCount = GetResponseCountForMultipleChoice(ds, Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString()), 0, j.ToString())
                            });

                        }


                    }

                    models.QuestionListForSelection.Add(new SelectListItem
                    {
                        Text = i + 1 + " - " + ds.Tables[0].Rows[i]["Question"].ToString().Substring(0, ds.Tables[0].Rows[i]["Question"].ToString().Length > 50 ? 50 : ds.Tables[0].Rows[i]["Question"].ToString().Length),
                        Value = ds.Tables[0].Rows[i]["ID"].ToString(),
                    });


                    if (models.QuestionID > 0 && models.QuestionID == Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString()))
                    {
                        models.QuestionList.Add(new QuestionListModes
                        {
                            OptionList = _OptionList,
                            QuestionType = ds.Tables[0].Rows[i]["QType"].ToString(),
                            QuestionText = ds.Tables[0].Rows[i]["Question"].ToString(),
                        });

                    }
                    else if (models.QuestionID == 0)
                    {
                        models.QuestionList.Add(new QuestionListModes
                        {
                            OptionList = _OptionList,
                            QuestionType = ds.Tables[0].Rows[i]["QType"].ToString(),
                            QuestionText = ds.Tables[0].Rows[i]["Question"].ToString(),
                        });
                    }
                }
            }
            return View(models);
        }


        private Random _random = new Random();





        private string GetResponseCountForSingleChoice(DataSet ds, int TableIndex, int RowIndex, string OptionValue)
        {
            DataRow[] rows = ds.Tables[TableIndex].Select("QResponse = '" + OptionValue.Replace("'", "’") + "'");

            if (rows.Length > 0)
            {
                return rows[0]["TCount"].ToString();
            }



            return "";
        }

        private string GetResponseCountForMultipleChoice(DataSet ds, int Question_ID, int RowIndex, string OptionValue)
        {
            DataRow[] rows = ds.Tables[31].Select("Question_ID = " + Question_ID);

            if (rows.Length > 0)
            {
                int Count = 0;

                foreach (var l in rows)
                {
                    var kkk = l["QResponse"].ToString();
                    if (Array.IndexOf(l["QResponse"].ToString().Split(','), OptionValue) > -1)
                    {
                        Count = Count + 1;
                    }
                }

                if (Count < 1)
                    return "";
                return Count.ToString();
            }



            return "";
        }

    }
}