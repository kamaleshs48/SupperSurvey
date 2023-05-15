using SuperSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using SuperSurvey.Repository;
using Newtonsoft.Json;
using System.Web;
namespace SuperSurvey.Controllers
{
    public class SurveyController : ApiController
    {

        [HttpGet]
        [Route("api/GetAreaCityVillageList")]
        public HttpResponseMessage GetAreaCityVillageList(int UserID, int StateID)
        {
            HttpResponseMessage _Response = new HttpResponseMessage();

            return Request.CreateResponse(HttpStatusCode.OK, CommonFunction.GetAreaCityVillageList(null));

        }




        [HttpGet]
        [Route("api/DemoUserRegister")]
        public HttpResponseMessage DemoUserRegister(string Email, string FirstName, string LastName, string Mobile1, string Mobile2, string Address = "")
        {
            HttpResponseMessage _Response = new HttpResponseMessage();
            LoginModels models = new LoginModels();

            models = CommonFunction.DemoUserUpdate(FirstName, LastName, Mobile1, Mobile2, Email, Address);



            return Request.CreateResponse(HttpStatusCode.OK, models);

        }








        [HttpGet]
        [Route("api/Register")]
        public HttpResponseMessage UserRegister(string Email, string FirstName, string LastName, string Password, string MobileNo, string Address = "")
        {
            HttpResponseMessage _Response = new HttpResponseMessage();
            LoginModels models = new LoginModels();

            models = CommonFunction.UserRegister(Email, FirstName, LastName, Password, MobileNo, Address);



            return Request.CreateResponse(HttpStatusCode.OK, models);

        }

        [HttpGet]
        [Route("api/GetLoksabhaList")]
        public HttpResponseMessage GetLoksabhaList(int UserID, int StateID)
        {
            HttpResponseMessage _Response = new HttpResponseMessage();

            return Request.CreateResponse(HttpStatusCode.OK, CommonFunction.GetLokSabhaList(StateID));

        }
        [HttpGet]
        [Route("api/GetWardBlockList")]
        public HttpResponseMessage GetWardBlockList(int UserID, int Vidhansabha_ID = 0)
        {
            HttpResponseMessage _Response = new HttpResponseMessage();
            return Request.CreateResponse(HttpStatusCode.OK, CommonFunction.GetWardBlockList(null));
        }

        [HttpGet]
        [Route("api/GetVidhansabhaList")]
        public HttpResponseMessage GetVidhansabhaList(int UserID, int Loksabha_ID = 0)
        {
            HttpResponseMessage _Response = new HttpResponseMessage();
            return Request.CreateResponse(HttpStatusCode.OK, CommonFunction.GetVidhanSabhaList(Loksabha_ID));

        }
        [HttpGet]
        [Route("api/GetStateList")]
        public HttpResponseMessage GetStateList(int UserID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, CommonFunction.GetStateList(null));

        }

        [HttpGet]
        [Route("api/ForgotPassword")]
        public HttpResponseMessage ForgotPassword(string MobileNo)
        {


            return Request.CreateResponse(HttpStatusCode.OK, CommonFunction.ForgotPassword(MobileNo));
        }
        [HttpGet]
        [Route("api/ResendOTP")]
        public HttpResponseMessage ResendOTP(string OTP, string MobileNo)
        {

            string Message = "Dear User Your mobile verification code is " + OTP + " Thank You. Mega Survey";
            CommonFunction.SendSMS(MobileNo, Message);
            return Request.CreateResponse(HttpStatusCode.OK, "Suessss");
        }


        [HttpGet]
        [Route("api/VerifyMobileNo")]
        public HttpResponseMessage VerifyMobileNo(string MobileNo)
        {


            CommonFunction.ExcuteCommond("update tbl_UserMaster set IsMobileVerify=1 , IsUserLogin=1 WHERE User_Name='" + MobileNo + "'");
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }


        [HttpGet]
        [Route("api/LogOut")]
        public HttpResponseMessage LogOut(string MobileNo)
        {
            CommonFunction.ExcuteCommond("update tbl_UserMaster set IsUserLogin = 0 , IsMobileVerify = 0 WHERE User_Name='" + MobileNo + "'");
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpGet]
        [Route("api/LoadData")]
        public HttpResponseMessage LoadData(int UserID)
        {


            DataSet ds = CommonFunction.LoadData(UserID);


            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("State", CommonFunction.GetStateList(ds.Tables[0]));
            obj.Add("LokSabha", CommonFunction.GetLoksabhaList(ds.Tables[1]));
            obj.Add("VidhanSabha", CommonFunction.GetVidhansabhaList(ds.Tables[2]));
            obj.Add("Word", CommonFunction.GetWardBlockList(ds.Tables[3]));
            obj.Add("Booth", CommonFunction.GetBoothList(ds.Tables[4]));
            obj.Add("Area", CommonFunction.GetAreaCityVillageList(ds.Tables[5]));
            obj.Add("SurveyList", JsonConvert.SerializeObject(ds.Tables[6]));
            obj.Add("PartyList", JsonConvert.SerializeObject(ds.Tables[7]));

            obj.Add("Language", JsonConvert.SerializeObject(ds.Tables[8]));
            obj.Add("Gender", JsonConvert.SerializeObject(ds.Tables[9]));
            obj.Add("AgeGroup", JsonConvert.SerializeObject(ds.Tables[10]));
            obj.Add("Religion", JsonConvert.SerializeObject(ds.Tables[11]));
            obj.Add("SubCaste", JsonConvert.SerializeObject(ds.Tables[12]));
            obj.Add("LanguageRegional", JsonConvert.SerializeObject(ds.Tables[13]));
            obj.Add("Education", JsonConvert.SerializeObject(ds.Tables[14]));
            obj.Add("Profession", JsonConvert.SerializeObject(ds.Tables[15]));
            obj.Add("PageMaster", JsonConvert.SerializeObject(ds.Tables[16]));
            obj.Add("QuestionMaster", JsonConvert.SerializeObject(ds.Tables[17]));
            obj.Add("QuestionResponse", JsonConvert.SerializeObject(ds.Tables[18]));
            obj.Add("SurveyAssignQuestion", JsonConvert.SerializeObject(ds.Tables[19]));

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }


        [HttpGet]
        [Route("api/Login")]
        public HttpResponseMessage Login(string email, string password, string DeviceID)
        {

            LoginModels models = new LoginModels();

            models = CommonFunction.Login(email, password, DeviceID);


            //if(email.ToLower()=="admin" && password.ToLower()=="admin")
            //{
            //    models.status = "success";
            //}
            //else
            //{
            //    models.status = "Fails";
            //}

            return Request.CreateResponse(HttpStatusCode.OK, models);
        }
        [HttpGet]
        [Route("api/SendFeedBack")]
        public HttpResponseMessage SendFeedBack(string FeedBack, string UserID, string MobileNo)

        {

            try
            {


                string _Body = "Dear Team, <br/> <b>Mega Survey Feedback</b> <br/>" + FeedBack;

                DataSet ds = new DataSet();

                string _SIGN = "<br/> <br/> <br/>Thanks & Regards <br/>";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + MobileNo + "  " + UserID + "','FeedBack" + UserID + "')");

                if (Convert.ToInt32(UserID) == 1)
                {

                    ds = CommonFunction.GetDataSet("SELECT * FROM tbl_DemoUserMaster WHERE Mobile1='" + MobileNo + "'");

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        _SIGN += "First Name : " + ds.Tables[0].Rows[0]["FirstName"].ToString() + "<br/>";
                        _SIGN += "Last Name : " + ds.Tables[0].Rows[0]["LastName"].ToString() + "<br/>";
                        _SIGN += "Mobile No : " + ds.Tables[0].Rows[0]["Mobile1"].ToString() + "<br/>";
                        _SIGN += "Alternate No : " + ds.Tables[0].Rows[0]["Mobile2"].ToString() + "<br/>";
                        _SIGN += "Email : " + ds.Tables[0].Rows[0]["Email"].ToString() + "<br/>";
                        _SIGN += "Address : " + ds.Tables[0].Rows[0]["Address"].ToString() + "<br/>";
                    }





                }
                else
                {
                    ds = CommonFunction.GetDataSet("SELECT * FROM tbl_UserMaster WHERE ID=" + UserID + "");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        _SIGN += "First Name : " + ds.Tables[0].Rows[0]["Name"].ToString() + "<br/>";
                        _SIGN += "Last Name : " + ds.Tables[0].Rows[0]["LastName"].ToString() + "<br/>";
                        _SIGN += "Mobile No : " + ds.Tables[0].Rows[0]["Mobile"].ToString() + "<br/>";
                        _SIGN += "Alternate No : " + ds.Tables[0].Rows[0]["Mobile_2"].ToString() + "<br/>";
                        _SIGN += "Email : " + ds.Tables[0].Rows[0]["Email"].ToString() + "<br/>";
                        _SIGN += "Address : " + ds.Tables[0].Rows[0]["Address"].ToString() + "<br/>";
                    }


                }



                _Body = _Body + _SIGN;


                CommonFunction.SendEmail(System.Configuration.ConfigurationSettings.AppSettings["ToEmail"].ToString(), "Mega Survey Feedback", _Body);


            }
            catch (Exception ex)

            {
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + ex.Message + "','FeedBack" + UserID + "')");
            }

            //    


            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }



        [HttpGet]
        [Route("api/GetDemoUser")]
        public HttpResponseMessage GetDemoUser()
        {
            LoginModels models = new LoginModels();


            DataSet ds = new DataSet();



            ds = CommonFunction.GetDataSet("Select * from tbl_UserMaster Where ID=1");


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                models.status = "success";
                models.EmailID = ds.Tables[0].Rows[0]["User_Name"].ToString();
                models.Password = ds.Tables[0].Rows[0]["User_Password"].ToString();

            }




            //if(email.ToLower()=="admin" && password.ToLower()=="admin")
            //{
            //    models.status = "success";
            //}
            //else
            //{
            //    models.status = "Fails";
            //}

            return Request.CreateResponse(HttpStatusCode.OK, models);
        }




        [HttpGet]
        [Route("api/SendDemoUserOTP")]

        public HttpResponseMessage SendDemoUserOTP(string MobileNo)
        {


            DataSet ds = new DataSet();
            LoginModels models = new LoginModels();
            users u = new users();

            String OTP = CommonFunction.CreateOTP(6);

            string Message = "Dear User Your mobile verification code is " + OTP + " Thank You. Mega Survey";
            models.ServerOTP = OTP;

            ds = CommonFunction.GetDataSet("Select * from tbl_DemoUserMaster Where Mobile1='" + MobileNo + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                CommonFunction.SendSMS(MobileNo, Message);
                u.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                u.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                u.MobileNo = ds.Tables[0].Rows[0]["Mobile1"].ToString();
                u.MobileNo1 = ds.Tables[0].Rows[0]["Mobile2"].ToString();
                u.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                u.Address = ds.Tables[0].Rows[0]["Address"].ToString();
            }
            else
            {
                CommonFunction.DemoUserRegister("", "", MobileNo, "", "", "");
                CommonFunction.SendSMS(MobileNo, Message);
                u.MobileNo = MobileNo;

            }
            models.status = "Success";

            models.users = u;



            return Request.CreateResponse(HttpStatusCode.OK, models);




        }



        // [HttpPost]
        [Route("api/PostData")]
        public HttpResponseMessage PostData()
        {
            string Data = "";

            int UserID = 1;
            var xx = Request.Content.ReadAsStringAsync();

            Data = xx.Result;


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + xx.Result + "','PostData123')");

            //      string ss = HttpContext.Current.Server.MapPath("~/UploadJSON/" + FileName + ".json");


            string Qry = string.Empty;


            try
            {

                List<MQResponse> MQRlist = new List<MQResponse>();
                //   string json = System.IO.File.ReadAllText(Server.MapPath("~/UploadJSON/T1.json"));

                // JsonConvert.DeserializeObject<List<RetrieveMultipleResponse>>(JsonStr);


                var ObjMain = JsonConvert.DeserializeObject<dynamic>(Data);

                string StrSurveyData = Convert.ToString(ObjMain[0]);
                string StrMultipleDataResponse = Convert.ToString(ObjMain[1]);


                var Obj = JsonConvert.DeserializeObject<List<SurveyDataModels>>(StrSurveyData);


                try
                {
                    MQRlist = JsonConvert.DeserializeObject<List<MQResponse>>(StrMultipleDataResponse);
                }
                catch
                {

                }
                //     var xxx = CommonFunction.ListToDataTable(Obj);

                // SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + xx.Result + "','PostData')");


                Qry = "INSERT INTO [tbl_SurveyData] " +
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
              " ,[CreatedDate],[Persone_ID],[Street],[Gender_ID],[Age_Group_ID],[Religion_ID],[Caste_ID],[Education_ID],[Profession_ID] ";

                Qry = Qry + @",[Email]
      ,[Name]
      ,[Mobile1]
      ,[Mobile2]
      ,[QResponse1]
      ,[QResponse2]
      ,[QResponse3]
      ,[QResponse4]
      ,[QResponse5]
      ,[QResponse6]
      ,[QResponse7]
      ,[QResponse8]
      ,[QResponse9]
      ,[QResponse10]
      ,[QResponse11]
      ,[QResponse12]
      ,[QResponse13]
      ,[QResponse14]
      ,[QResponse15]
      ,[QResponse16]
      ,[QResponse17]
      ,[QResponse18]
      ,[QResponse19]
      ,[QResponse20]
,[QResponse21]
,[QResponse22]
,[QResponse23]
,[QResponse24]
,[QResponse25]
,[QResponse26]
,[QResponse27]
,[QResponse28]
,[QResponse29]
,[QResponse30]
) VALUES";



                int MaxID = Convert.ToInt32(CommonFunction.GetRecordID("Select Max(ID) from tbl_SurveyData"));


                string Values = ",";
                foreach (var l in Obj)
                {
                    Values = Values + ",(" + (MaxID + Convert.ToInt32(l.RowID)) +
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
                         ", GetDate() , " + Convert.ToInt32(l.PersonID) + ",'" + l.Street.Replace("'", "") + "'" +
                        "," + l.Gender_ID +
                        "," + l.Age_Group_ID +
                        "," + l.Religion_ID +
                        "," + l.Caste_ID +
                        "," + l.Education_ID +
                        "," + l.Profession_ID +
                        ",'" + l.Email + "'" +
                        ",'" + l.Name + "'" +
                        ",'" + l.Mobile1 + "'" +
                        ",'" + l.Mobile2 + "'" +
                        ",'" + l.QResponse1 + "'" +
                        ",'" + l.QResponse2 + "'" +
                        ",'" + l.QResponse3 + "'" +
                        ",'" + l.QResponse4 + "'" +
                        ",'" + l.QResponse5 + "'" +
                        ",'" + l.QResponse6 + "'" +
                        ",'" + l.QResponse7 + "'" +
                        ",'" + l.QResponse8 + "'" +
                        ",'" + l.QResponse9 + "'" +
                        ",'" + l.QResponse10 + "'" +
                        ",'" + l.QResponse11 + "'" +
                        ",'" + l.QResponse12 + "'" +
                        ",'" + l.QResponse13 + "'" +
                        ",'" + l.QResponse14 + "'" +
                        ",'" + l.QResponse15 + "'" +
                        ",'" + l.QResponse16 + "'" +
                        ",'" + l.QResponse17 + "'" +
                        ",'" + l.QResponse18 + "'" +
                        ",'" + l.QResponse19 + "'" +
                        ",'" + l.QResponse20 + "'" +
                        ",'" + l.QResponse21 + "'" +
                        ",'" + l.QResponse22 + "'" +
                        ",'" + l.QResponse23 + "'" +
                        ",'" + l.QResponse24 + "'" +
                        ",'" + l.QResponse25 + "'" +
                        ",'" + l.QResponse26 + "'" +
                        ",'" + l.QResponse27 + "'" +
                        ",'" + l.QResponse28 + "'" +
                        ",'" + l.QResponse29 + "'" +
                        ",'" + l.QResponse30 + "'" + ")";
                }

                //var x = Values.Substring(Values.Trim().Length - 1);


                Qry = Qry + Values.Replace(",,", "");

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);


                foreach (var line in Obj.GroupBy(info => info.Survey_ID)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
                {
                    Qry = "UPDATE tbl_Survey_UserMap SET AblEntry = AblEntry +" + line.Count + " WHERE User_ID = " + UserID + " AND Survey_ID = " + line.Metric;

                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
                }

                Qry = "";
                foreach (var k in MQRlist)
                {
                    Qry = Qry + @"INSERT INTO [tbl_MQResponse]
           ([Question_ID]
           ,[Row_ID]
           ,[QResponse]
           ,[Survey_ID])
     VALUES (" + k.Question_ID + "," + (MaxID + Convert.ToInt32(k.SN)) + ",'" + k.QResponse + "'," + k.Survey_ID + "); ";
                }
                if (Qry != "")
                {
                    Qry += @" ; UPDATE t1
  SET t1.UserID = t2.User_ID
  FROM tbl_MQResponse AS t1
  INNER JOIN tbl_SurveyData AS t2
  ON t1.Row_ID = t2.Row_ID
  AND t1.Survey_ID = t2.Survey_ID";


                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);

                }


                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }

            catch (Exception ex)

            {

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + Qry + "','PostData')");
                return Request.CreateResponse(HttpStatusCode.OK, "Fails");
            }

            // return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
        [HttpGet]
        [Route("api/GetSurveyList")]
        public HttpResponseMessage GetSurveyList(int UserID)
        {

            DataSet ds = new DataSet();

            string _Qry = @"Select SD.ID, UM.User_Name 'UserName', SNM.Name 'SurveyName', PM.Name 'PartyName', 
SM.StateName,LM.Name 'LokSabhaName'  
,VM.NAME 'VidhansabhaName'  
,WM.Name 'WardBlock'  
,CV.Name 'Area'  
,SD.OtherArea  
,SD.GPSCoordinate  
,SD.SurveyDate 'SurveyDate'  
,SD.CreatedDate 'PostDate'  
 from tbl_SurveyData SD   
INNER Join tbl_UserMaster UM On Um.ID=SD.User_ID  
INNER JOIN tbl_SurveyNameMaster SNM ON SNM.ID=SD.Survey_ID  
INNER Join tbl_PartyMaster PM ON PM.ID=SD.Party_ID  
INNER Join tbl_StateMaster SM ON SM.ID=SD.State_ID  
LEFT JOIN tbl_Loksabha LM ON LM.ID=SD.LokSabha_ID  
LEFT JOIN  tbl_Vidhansabha VM ON VM.ID=SD.VidhanSabha_ID  
LEFT JOIN tbl_WordBlock WM  ON WM.ID=SD.Word_ID  
LEFT JOIN tbl_AreaCityVillage CV on CV.ID=SD.Area_ID  
ORDER BY SNM.Name,PM.Name";

            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, _Qry);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(ds.Tables[0]);



            return Request.CreateResponse(HttpStatusCode.OK, JSONString);
        }



        [Route("api/UpdateUserProfile")]
        public HttpResponseMessage UpdateUserProfile()
        {
            string Data = "";
            var xx = Request.Content.ReadAsStringAsync();
            Data = xx.Result;
            string Qry = string.Empty;
            LoginModels models = new LoginModels();
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + Data + "','Profile')");
            try
            {
                var Obj = JsonConvert.DeserializeObject<UserProfileModels>(Data);

                Qry = @"UPDATE tbl_UserMaster SET Name='" + Obj.FirstName + "', LastName='" + Obj.LastName + "', Mobile_2='" + Obj.AlternateMobileNo + "',Email='" +
                    Obj.Email + "',Address='" + Obj.Address + "' Where ID=" + Obj.UID;

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);



                try
                {

                    byte[] imageBytes = Convert.FromBase64String(Obj.ImagePath);

                    //Save the Byte Array as Image File.
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/IMG/UserPhoto/" + Obj.UID + "P.png");
                    System.IO.File.WriteAllBytes(filePath, imageBytes);

                    string _P = "http://api.kkminfotech.com/img/UserPhoto/" + Obj.UID + "p.png";

                    Qry = @"UPDATE tbl_UserMaster SET ImagePath='" + _P + "' Where ID=" + Obj.UID;
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);


                }
                catch (Exception ex)
                {
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + ex.Message + "','Profile Update')");
                }



                models = CommonFunction.GetUserProfile(Convert.ToInt32(Obj.UID));









                return Request.CreateResponse(HttpStatusCode.OK, models);
            }
            catch (Exception ex)
            {
                models.status = "Fails";
                models.ErrorMessage = "Oops! Something went wrong, please try again";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + Qry + "','PostData')");
                return Request.CreateResponse(HttpStatusCode.OK, models);
            }

            // return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [HttpGet]
        [Route("api/ChangePassword")]
        public HttpResponseMessage ChangePassword(string Password, string UserID)
        {

            // SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "INSERT INTO tbl_Error(ErrMasaage,ErrSource) Values('" + Password + " " + UserID + "','ChangePassword')");
            CommonFunction.ExcuteCommond("update tbl_UserMaster set User_Password='" + Password + "'  WHERE ID=" + UserID + "");
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }


    }
}
