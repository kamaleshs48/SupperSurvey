using SuperSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Mvc;
using System.Configuration;
using Dapper;
using System.Dynamic;
using System.Net;
using System.IO;
using System.Text;

namespace SuperSurvey.Repository
{
    public class CommonFunction
    {
        private readonly static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        public static bool SendSMS(string MobileNo, string Msg)
        {
            //http://smsprofessionals.com/sendsms?uname=yourUsername&pwd=yourPassword&senderid=yourSenderid&to=9444xxxxxx&msg=yourMessage&route=yourRoute
            //  string url = "http://smsprofessionals.com/sendsms?uname=rajivarora&pwd=123456&senderid=GRNCAR&to=7042771792&msg=Dear Dilipraj welcome to Green Car India. To complete the registration process we need to confirm your mobile number. Please sms Green at New Delhi to confirm the same.&route=A";
            string url = "http://smsprofessionals.com/sendsms?uname=rajivarora&pwd=123456&senderid=GRNCAR&to=" + MobileNo + "&msg=" + Msg + "&route=T";
            url = "http://173.45.76.227/send.aspx?username=rajivcarpool&pass=Green007&route=trans1&senderid=GRNCAR&numbers=" + MobileNo + "&message=" + Msg;

            url = "http://hindit.co.in/API/pushsms.aspx?loginID=t1megamind&password=megamind@123&mobile=" + MobileNo + "&text=" + Msg + "&senderid=MGSRVE&route_id=2&Unicode=0&IP=1.1.1.1";


            bool success = false;
            try
            {

                int outn = 0;
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream receiveStream = response.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        string line = readStream.ReadToEnd().Trim();
                        success = Int32.TryParse(line, out outn);
                    }
                }

            }
            catch
            {
                success = false;
            }
            return success;
        }
        public static string CreateOTP(int OTPLength)
        {
            string _allowedChars = "0123456789";
            Random randNum = new Random();
            char[] chars = new char[OTPLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < OTPLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public static List<dynamic> ConvertDtToList(DataTable dt)
        {
            var data = new List<dynamic>();
            foreach (var item in dt.AsEnumerable())
            {
                // Expando objects are IDictionary<string, object>  
                IDictionary<string, object> dn = new ExpandoObject();

                foreach (var column in dt.Columns.Cast<DataColumn>())
                {
                    dn[column.ColumnName] = item[column];
                }

                data.Add(dn);
            }
            return data;
        }

        public static DataSet GetDataSet(string Qry)
        {
            return SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
        }
        public static int ExcuteCommond(string Qry)
        {
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
        }


        public static List<NameCountModels1> GetSurveyReport1(int Survey_ID)
        {
            SqlParameter[] pr = new SqlParameter[]
               {
                        new SqlParameter("@Survey_ID",Survey_ID),
               };


            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_GenerateReport1", pr);


            List<NameCountModels1> _list = new List<NameCountModels1>();


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _list.Add(new NameCountModels1
                    {
                        ID = Convert.ToInt32(dr["MID"].ToString()),
                        Name = dr["Name"].ToString(),
                        PID = Convert.ToInt32(dr["PID"].ToString()) == 0 ? (int?)null : Convert.ToInt32(dr["PID"].ToString()),
                        Count = dr["TCount"].ToString(),

                    });

                }

            }

            return _list;
        }


        public static int GetIdentity(ref int ID)
        {
            ID = ID + 1;
            return ID;
        }

        public static int GetDTMaxID(DataTable dt)
        {
            return Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ID"].ToString());
        }

        internal static string ForgotPassword(string mobileNo)
        {

            DataSet ds = new DataSet();
            string Message = "";
            string ReturnMessage = "";
            try
            {
                ds = GetDataSet("Select * from tbl_UserMaster Where Mobile='" + mobileNo + "'");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //Dear user your username is 8510909798 and Password is gynchnf. Thanks & Regards Mega Survey.

                    Message = "Dear user your username is " + mobileNo + " and Password is " + ds.Tables[0].Rows[0]["User_Password"].ToString() + " .Thanks You. Mega Survey.";
                    CommonFunction.SendSMS(mobileNo, Message);
                    CommonFunction.ExcuteCommond("update tbl_UserMaster set IsMobileVerify=0 where User_Name='" + mobileNo + "'");
                    ReturnMessage = "Password sent successfully to your Mobile No.";
                }
                else
                {
                    ReturnMessage = "Mobile No not Exist.";
                }
            }
            catch
            {
                ReturnMessage = "Somthing went wrong, Please Try again some time.";
            }

            return ReturnMessage;




        }





        public static int GetDTRecordID(DataTable dt, string Name)
        {

            return 0;
        }


        public static List<NameCountModels1> GetSurveyReport2(int Survey_ID)
        {
            SqlParameter[] pr = new SqlParameter[]
               {
                        new SqlParameter("@Survey_ID",Survey_ID),
               };

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "select * from  tbl_Report", pr);
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            DataSet ds4 = new DataSet();
            DataSet ds5 = new DataSet();
            DataSet ds6 = new DataSet();
            DataSet dsParty = new DataSet();







            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            column.ColumnName = "ID";

            // Add the column to a new DataTable.
            DataTable DTMaster = new DataTable();
            DTMaster.Columns.Add(column);

            DTMaster.Columns.Add(new DataColumn { ColumnName = "MID", DataType = typeof(Int32) });
            DTMaster.Columns.Add(new DataColumn { ColumnName = "PID", DataType = typeof(Int32) });
            DTMaster.Columns.Add(new DataColumn { ColumnName = "Name", DataType = typeof(string) });
            DTMaster.Columns.Add(new DataColumn { ColumnName = "TCount", DataType = typeof(Int32) });
            DTMaster.Columns.Add(new DataColumn { ColumnName = "ACount", DataType = typeof(Int32) });
            DTMaster.Columns.Add(new DataColumn { ColumnName = "RLevel", DataType = typeof(Int32) });



            //Bind State

            string Qry = @"Select 
SM.ID,
SM.StateName Name ,COUNT(SD.ID) TCOUNT ,SUM(COUNT(SD.ID)) OVER() AS total_count
from tbl_SurveyData SD   
INNER Join tbl_StateMaster SM ON SM.ID=SD.State_ID 
WHERE SD.Survey_ID= " + Survey_ID + " Group By StateName,SM.ID";


            ds = GetDataSet(Qry);
            DataRow _dr = DTMaster.NewRow();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                _dr = DTMaster.NewRow();
                _dr[1] = dr[0];
                _dr[2] = 0;
                _dr[3] = dr[1];
                _dr[4] = dr[2];
                _dr[5] = dr[3];
                _dr[6] = 1;
                DTMaster.Rows.Add(_dr);

                Qry = @"Select 
                SM.ID,
                SM.Name ,COUNT(SD.ID) TCOUNT,SUM(COUNT(SD.ID)) OVER() AS total_count 
                from tbl_SurveyData SD   
                INNER Join tbl_PartyMaster  SM ON SM.ID=SD.Party_ID 
                WHERE SD.Survey_ID= " + Survey_ID + " AND SD.State_ID=" + dr["ID"].ToString() + " Group By Name,SM.ID";

                // Add Party Text 


                var _pdr = DTMaster.NewRow();
                _pdr[1] = 0;
                _pdr[2] = GetDTMaxID(DTMaster);
                _pdr[3] = "Party";
                _pdr[4] = 0;
                _pdr[5] = 0;
                _pdr[6] = 2;
                DTMaster.Rows.Add(_pdr);


                dsParty = GetDataSet(Qry);

                foreach (DataRow drr in dsParty.Tables[0].Rows)
                {
                    _pdr = DTMaster.NewRow();
                    _pdr[1] = drr[0];
                    _pdr[2] = GetDTRecordID(DTMaster, "Party");
                    _pdr[3] = drr[1];
                    _pdr[4] = drr[2];
                    _pdr[5] = drr[3];
                    _pdr[6] = 2;
                    DTMaster.Rows.Add(_pdr);
                }


            }
            //Bind Party






            List<NameCountModels1> _list = new List<NameCountModels1>();

            string _MainQry = "truncate table tbl_Report;";
            string _Qry = @"Select 
SM.ID,
SM.StateName Name ,COUNT(SD.ID) TCOUNT ,SUM(COUNT(SD.ID)) OVER() AS total_count
from tbl_SurveyData SD   
INNER Join tbl_StateMaster SM ON SM.ID=SD.State_ID 
WHERE SD.Survey_ID= " + Survey_ID + " Group By StateName,SM.ID";

            ds1 = GetDataSet(_Qry);

            int Index = 0;
            int _ID = 0;
            int _PID = 0;
            int _MPID = 0;
            int StateID = 0;
            int PartyID, LokSabhaID, VidhanSabhaID = 0;




            _PID = 0;
            //Insert State
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {

                _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _PID + ",'" + dr["Name"].ToString() + "'," + dr["TCOUNT"].ToString() + ") ;";


                _Qry = @"Select 
                SM.ID,
                SM.Name ,COUNT(SD.ID) TCOUNT 
                from tbl_SurveyData SD   
                INNER Join tbl_PartyMaster  SM ON SM.ID=SD.Party_ID 
                WHERE SD.Survey_ID= " + Survey_ID + " AND SD.State_ID=" + dr["ID"].ToString() + " Group By Name,SM.ID";

                _PID = _ID;

                dsParty = GetDataSet(_Qry);
                _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _PID + ",'Party'," + ds1.Tables[0].Rows[0]["TCOUNT"].ToString() + ") ;";
                // Insert Party
                int Party_PID = _ID;
                foreach (DataRow dr2 in dsParty.Tables[0].Rows)
                {

                    _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + Party_PID + ",'" + dr2["Name"].ToString() + "'," + dr2["TCOUNT"].ToString() + ") ;";
                }

                // Inser LOkSabha
                StateID = Convert.ToInt32(dr["ID"].ToString());
                _Qry = @"Select 
                SM.ID,
                SM.Name ,COUNT(SD.ID) TCOUNT 
                from tbl_SurveyData SD   
                INNER Join tbl_Loksabha  SM ON SM.ID=SD.LokSabha_ID 
                WHERE SD.Survey_ID= " + Survey_ID + " AND SD.State_ID=" + dr["ID"].ToString() + " Group By Name,SM.ID";
                ds3 = GetDataSet(_Qry);


                _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _PID + ",'LokSabha'," + ds1.Tables[0].Rows[0]["TCOUNT"].ToString() + ") ;";

                _MPID = _ID;

                _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _MPID + ",'Party'," + ds3.Tables[0].Rows[0]["TCOUNT"].ToString() + ") ;";

                _PID = _ID;


                _Qry = @"Select 
                SM.ID,
                SM.Name ,COUNT(SD.ID) TCOUNT 
                from tbl_PartyMaster  SM  INNER Join tbl_SurveyData SD   
                 ON SM.ID=SD.Party_ID 
                WHERE SD.Survey_ID= " + Survey_ID + " AND SD.State_ID=" + dr["ID"].ToString() + "  Group By Name,SM.ID";
                dsParty = GetDataSet(_Qry);
                foreach (DataRow dr2 in dsParty.Tables[0].Rows)
                {

                    _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _PID + ",'" + dr2["Name"].ToString() + "'," + dr2["TCOUNT"].ToString() + ") ;";
                }


                //_MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _MPID + ",'Vidhan Sabha'," + ds3.Tables[0].Rows[0]["TCOUNT"].ToString() + ") ;";
                //_PID = _ID;
                //Insert LokSabha 
                foreach (DataRow dr3 in ds3.Tables[0].Rows)
                {
                    PartyID = Convert.ToInt32(dr["ID"].ToString());
                    _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _MPID + ",'" + dr3["Name"].ToString() + "'," + dr3["TCOUNT"].ToString() + ") ;";

                    _PID = _ID;
                    _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _PID + ",'Party'," + ds3.Tables[0].Rows[0]["TCOUNT"].ToString() + ") ;";

                    _PID = _ID;


                    _Qry = @"Select 
                SM.ID,
                SM.Name ,COUNT(SD.ID) TCOUNT 
                from tbl_PartyMaster  SM  INNER Join tbl_SurveyData SD   
                 ON SM.ID=SD.Party_ID 
                WHERE SD.Survey_ID= " + Survey_ID + " AND SD.State_ID=" + dr["ID"].ToString() + "  Group By Name,SM.ID";
                    dsParty = GetDataSet(_Qry);
                    foreach (DataRow dr2 in dsParty.Tables[0].Rows)
                    {

                        _MainQry = _MainQry + "INSERT INTO [tbl_Report] VALUES(" + GetIdentity(ref _ID) + "," + _PID + ",'" + dr2["Name"].ToString() + "'," + dr2["TCOUNT"].ToString() + ") ;";
                    }

                    _Qry = @"Select 
                SM.ID,
                SM.Name ,COUNT(SD.ID) TCOUNT 
                from tbl_PartyMaster  SM  INNER Join tbl_SurveyData SD   
                 ON SM.ID=SD.Party_ID 
                WHERE SD.Survey_ID= " + Survey_ID + " AND SD.State_ID=" + dr["ID"].ToString() + "  Group By Name,SM.ID";
                    dsParty = GetDataSet(_Qry);
                    _PID = _ID;




                    _Qry = @"Select 
                                SM.ID,
                                SM.Name ,COUNT(SD.ID) TCOUNT 
                                from tbl_SurveyData SD   
                                INNER Join tbl_Vidhansabha  SM ON SM.ID=SD.VidhanSabha_ID 
                                WHERE SD.Survey_ID= " + Survey_ID + " AND SD.LokSabha_ID=" + dr["ID"] + " Group By Name,SM.ID";
                    ds4 = GetDataSet(_Qry);






                }



            }




            ExcuteCommond(_MainQry);



            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "select * from  tbl_Report", pr);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _list.Add(new NameCountModels1
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                        PID = Convert.ToInt32(dr["PID"].ToString()) == 0 ? (int?)null : Convert.ToInt32(dr["PID"].ToString()),
                        Count = dr["TCount"].ToString(),

                    });

                }

            }

            return _list;
        }


        public static SurveyMasterModels GetSurveyDetails(int SurveyID)
        {
            DataSet ds = new DataSet();


            SqlParameter[] pr = new SqlParameter[]
                {
                        new SqlParameter("@Survey_ID",SurveyID),
                };



            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_GetSurveyDetails", pr);
            SurveyMasterModels models = new SurveyMasterModels();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                models.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                models.Description = ds.Tables[0].Rows[0]["Discreption"].ToString();
                models.Survey_For_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Survey_For_ID"].ToString());
                models.Survey_Type_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Survey_Type_ID"].ToString());

                List<CheckListBoxModels> _list = new List<CheckListBoxModels>();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        _list.Add(new CheckListBoxModels { Text = dr["Name"].ToString(), Value = dr["PartyID"].ToString(), Order_Index = dr["ORDER_ID"].ToString() });
                    }
                }
                models.PartyList = _list;

            }

            return models;
        }


        public static string GetRecordID(string Qry)
        {
            string ReturnValue = "0";
            try
            {

                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
                ReturnValue = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                ReturnValue = "0";
            }
            return ReturnValue;
        }


        public static int SurveyAssign(SurveyAssingModels models)
        {
            try
            {
                SqlParameter[] pr = new SqlParameter[]
                {
                        new SqlParameter("@Survey_ID",models.SurveyID),
                        new SqlParameter("@ORG_ID",models.ORG_ID),
                        new SqlParameter("@State_ID",models.StateID),
                        new SqlParameter("@Loksabha_ID",models.Loksabha_ID),
                        new SqlParameter("@Vidhansabha_ID",models.Vidhansabha_ID),
                        new SqlParameter("@WardBlock_ID",models.WardBlock_ID),
                        new SqlParameter("@ExpDate",models.ExpDate),
                        new SqlParameter("@MaxEntry",models.MaxLimit),
                        new SqlParameter("@User_ID",models.UserIDS)


                };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_SurveyAssign", pr);
                // return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
            }
            catch (Exception ex)
            {

            }

            return 1;


        }


        public static int RegisterUser(UserRegisterModels models)
        {
            try
            {
                SqlParameter[] pr = new SqlParameter[]
                {
                    new SqlParameter("@Name",models.Name),
                    new SqlParameter("@User_Name",models.User_Name),
                    new SqlParameter("@Email",models.Email),
                    new SqlParameter("@Mobile",models.MobileNo1),
                    new SqlParameter("@Mobile_2",models.MobileNo2),
                    new SqlParameter("@User_Type","SurveyUser"),
                    new SqlParameter("@User_Password",models.Password),
                    new SqlParameter("@IsActive",1),
                    new SqlParameter("@ORG_ID",models.ORG_ID),
                    new SqlParameter("@Address",models.Address),
                    new SqlParameter("@Mode","Save"),

                };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_SurveyAddUser", pr);
            }
            catch (Exception ex)
            {

            }

            return 1;
        }








        public static IEnumerable<ReportDataModels> GetSurveyData(int pageIndex, int pageSize)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn_Str"].ToString());
            var para = new DynamicParameters();
            para.Add("@PageIndex", pageIndex);
            para.Add("@PageSize", pageSize);
            var employees = con.Query<ReportDataModels>("sp_GetSurveyData", para, commandType: CommandType.StoredProcedure);
            return employees;
        }


        public static List<UserRegisterModels> GetUserList(int ORG_ID)
        {
            List<UserRegisterModels> _List = new List<UserRegisterModels>();
            try
            {
                SqlParameter[] pr = new SqlParameter[]
                {
                    new SqlParameter("@ORG_ID",ORG_ID),
                    new SqlParameter("@Mode","GetUserList"),

                };
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_SurveyAddUser", pr);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _List.Add(new UserRegisterModels
                        {
                            UserID = Convert.ToInt32(dr["ID"].ToString()),
                            Name = dr["Name"].ToString(),
                            User_Name = dr["User_Name"].ToString(),
                            Email = dr["Email"].ToString(),
                            MobileNo1 = dr["Mobile"].ToString(),
                            MobileNo2 = dr["Mobile_2"].ToString(),
                            Password = dr["User_Password"].ToString(),
                            Address = dr["Address"].ToString(),

                        });

                    }

                }



            }
            catch (Exception ex)
            {

            }

            return _List;
        }



        public static List<UserRegisterModels> GetUserListForSurveyAssign(int ORG_ID, int SurveyID = 0)
        {
            List<UserRegisterModels> _List = new List<UserRegisterModels>();
            try
            {
                SqlParameter[] pr = new SqlParameter[]
                {
                     new SqlParameter("@ORG_ID",ORG_ID),
                      new SqlParameter("@SurveyID",SurveyID),

                    new SqlParameter("@Mode","GetUserListForSurveyAssign"),

                };
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_SurveyAddUser", pr);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _List.Add(new UserRegisterModels
                        {
                            UserID = Convert.ToInt32(dr["ID"].ToString()),
                            Name = dr["Name"].ToString(),
                            User_Name = dr["User_Name"].ToString(),
                            Email = dr["Email"].ToString(),
                            MobileNo1 = dr["Mobile"].ToString(),
                            MobileNo2 = dr["Mobile_2"].ToString(),
                            Password = dr["User_Password"].ToString(),
                            Address = dr["Address"].ToString(),
                            IsChecked = Convert.ToUInt32(dr["IsAssign"].ToString()) == 1 ? true : false,
                        });

                    }

                }



            }
            catch (Exception ex)
            {

            }

            return _List;
        }

        public static List<SelectModels> GetStateList(DataTable dt)
        {
            SelectModels models = new SelectModels();


            foreach (DataRow dr in dt.Rows)
            {
                models.GList.Add(new SelectModels
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    PID = 0,
                    Code = dr["Code"].ToString(),
                    Name = dr["StateName"].ToString()
                });
            }
            return models.GList;
        }


        public static List<SelectListItem> Survey_For_List()
        {
            List<SelectListItem> _List = new List<SelectListItem>();
            try
            {


                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "SELECT * FROM tbl_SurveyFor");


                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow l in ds.Tables[0].Rows)
                    {
                        _List.Add(new SelectListItem
                        {
                            Text = l["Survey_For"].ToString(),
                            Value = l["ID"].ToString(),


                        });

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return _List;
        }


        public static List<SelectModels> GetLoksabhaList(DataTable dt)
        {
            SelectModels models = new SelectModels();


            foreach (DataRow dr in dt.Rows)
            {
                models.GList.Add(new SelectModels
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    PID = Convert.ToInt32(dr["State_ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString()
                });
            }
            return models.GList;
        }
        public static List<SelectModels> GetVidhansabhaList(DataTable dt)
        {
            SelectModels models = new SelectModels();


            foreach (DataRow dr in dt.Rows)
            {
                models.GList.Add(new SelectModels
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    PID = Convert.ToInt32(dr["Loksabha_ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString()
                });
            }
            return models.GList;
        }
        public static List<SelectModels> GetWardBlockList(DataTable dt)
        {
            SelectModels models = new SelectModels();


            foreach (DataRow dr in dt.Rows)
            {
                models.GList.Add(new SelectModels
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    PID = Convert.ToInt32(dr["Vidhansabha_ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString()
                });
            }
            return models.GList;
        }


        public static List<SelectModels> GetBoothList(DataTable dt)
        {
            SelectModels models = new SelectModels();


            foreach (DataRow dr in dt.Rows)
            {
                models.GList.Add(new SelectModels
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    PID = Convert.ToInt32(dr["WardBlock_ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString()
                });
            }
            return models.GList;
        }




        public static List<SelectModels> GetAreaCityVillageList(DataTable dt)
        {
            SelectModels models = new SelectModels();


            foreach (DataRow dr in dt.Rows)
            {
                models.GList.Add(new SelectModels
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    PID = Convert.ToInt32(dr["WardBlock_ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString()
                });
            }
            return models.GList;
        }

        public static LoginModels Login(string Email, string Password, string DeviceID)
        {
            LoginModels models = new LoginModels();
            DataSet ds = new DataSet();
            users u = new users();
            try
            {



                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "SELECT * FROM tbl_UserMaster WHERE User_Name='" + Email + "' AND User_Password='" + Password + "'");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    Random random = new Random();
                    models.status = "success";
                    u.UID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    u.FirstName = ds.Tables[0].Rows[0]["Name"].ToString();
                    u.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    u.Email = ds.Tables[0].Rows[0]["EMail"].ToString();
                    u.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    u.Gender = ds.Tables[0].Rows[0]["Mobile"].ToString();

                    u.ImagePath = ds.Tables[0].Rows[0]["ImagePath"].ToString() + "?v=" + random.Next(1000);
                    u.EmailVerifiy = ds.Tables[0].Rows[0]["EMail"].ToString();
                    u.Password = ds.Tables[0].Rows[0]["User_Password"].ToString();
                    u.alpha2Code = ds.Tables[0].Rows[0]["EMail"].ToString();
                    models.IsMobileVerify = ds.Tables[0].Rows[0]["IsMobileVerify"].ToString();
                    models.MobileNo = u.MobileNo;
                    u.MobileNo1 = ds.Tables[0].Rows[0]["Mobile_2"].ToString();
                    u.Address = ds.Tables[0].Rows[0]["Address"].ToString();






                    models.users = u;
                    models.status = "success";

                    if (ds.Tables[0].Rows[0]["IsUserLogin"].ToString() == "1")
                    {
                        models.status = "Fails";
                        models.ErrorMessage = "User is already Login with other mobile. Please Logout and Login again.";
                    }
                    else if (models.IsMobileVerify == "0")
                    {
                        string _OTP = CommonFunction.CreateOTP(6);
                        //Dear User Kindly confirm your Mobile No by entering verification code 647579 in your account. Thank You.
                        //Dear User Kindly confirm your Mobile No by entering verification code 647579 in your account. Thank You. Mega Survey
                        string Message = "Dear User Kindly confirm your Mobile No by entering verification code " + _OTP + " in your account. Thank You. Mega Survey";
                        CommonFunction.SendSMS(u.MobileNo, Message);
                        models.ServerOTP = _OTP;
                    }
                    else
                    {
                        CommonFunction.ExcuteCommond("UPDATE tbl_UserMaster SET IsUserLogin=1,DeviceID='" + DeviceID + "' WHERE User_Name='" + u.MobileNo + "'");
                        // CommonFunction.SendSMS(u.MobileNo, "");
                    }


                }
                else
                {
                    models.status = "Fails";
                    models.ErrorMessage = "Invalid UserID or Password.";
                }






            }

            ////////    SqlParameter[] pr = new SqlParameter[]
            ////////    {
            ////////    new SqlParameter("@username", Email),
            ////////    new SqlParameter("@password", Password),
            ////////    new SqlParameter("@flag1", "b"),

            ////////    };
            ////////    SqlDataReader rd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "LoginUser", pr);
            ////////    if (rd.HasRows)
            ////////    {
            ////////        models.status = "Success";

            ////////        rd.Read();
            ////////        // Start Save Data In Session
            ////////        u.UID = Convert.ToInt32(rd["ID"].ToString());
            ////////        u.FirstName = rd["FirstName"].ToString();
            ////////        u.Email = rd["Email"].ToString();
            ////////        u.MobileNo = rd["Mobile"].ToString();
            ////////        u.Gender = rd["Gender"].ToString();
            ////////        u.OTP = Convert.ToInt32(rd["OTP"].ToString());
            ////////        u.ImagePath = rd["PhotoPath"].ToString();
            ////////        u.EmailVerifiy = rd["emailverify"].ToString();
            ////////        u.Password = rd["Password"].ToString();
            ////////        u.alpha2Code = rd["alpha2Code"].ToString();
            ////////        models.users = u;
            ////////        models.status = "Success";
            ////////    }
            ////////    else
            ////////    {
            ////////        models.status = "Fails";
            ////////        models.ErrorMessage = "Invalid Email or Password.";
            ////////    }
            ////////}
            catch
            {
                ////////    models.status = "Fails";
                ////////    models.ErrorMessage = "Invalid Email or Password.";
                ////////    models.users = u;
            }




            return models;
        }






        public static LoginModels GetUserProfile(int UserID)
        {
            LoginModels models = new LoginModels();
            DataSet ds = new DataSet();
            users u = new users();
            try
            {



                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "SELECT * FROM tbl_UserMaster WHERE ID=" + UserID + "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    Random random = new Random();
                    models.status = "success";
                    u.UID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    u.FirstName = ds.Tables[0].Rows[0]["Name"].ToString();
                    u.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    u.Email = ds.Tables[0].Rows[0]["EMail"].ToString();
                    u.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    u.Gender = ds.Tables[0].Rows[0]["Mobile"].ToString();

                    u.ImagePath = ds.Tables[0].Rows[0]["ImagePath"].ToString() + "?v=" + random.Next(1000);
                    u.EmailVerifiy = ds.Tables[0].Rows[0]["EMail"].ToString();
                    u.Password = ds.Tables[0].Rows[0]["User_Password"].ToString();
                    u.alpha2Code = ds.Tables[0].Rows[0]["EMail"].ToString();
                    models.IsMobileVerify = ds.Tables[0].Rows[0]["IsMobileVerify"].ToString();
                    models.MobileNo = u.MobileNo;
                    u.MobileNo1 = ds.Tables[0].Rows[0]["Mobile_2"].ToString();
                    u.Address = ds.Tables[0].Rows[0]["Address"].ToString();


                    models.users = u;
                    models.status = "success";




                }
                else
                {
                    models.status = "Fails";
                    models.ErrorMessage = "Oops! Something went wrong, please try again";
                }






            }


            catch
            {
                ////////    models.status = "Fails";
                ////////    models.ErrorMessage = "Invalid Email or Password.";
                ////////    models.users = u;
            }




            return models;
        }

        public static LoginModels UserRegister(string Email, string FirstName, string LastName, string Password, string MobileNo, string Address)
        {

            LoginModels models = new LoginModels();
            users u = new users();


            if (Convert.ToInt32(GetRecordID("Select count(*) from tbl_UserMaster where Mobile='" + MobileNo + "'")) > 0)
            {
                models.status = "Fails";
                models.ErrorMessage = "Mobile no already registerd.";
                return models;
            }


            try
            {





                string Qry = @"INSERT INTO [tbl_UserMaster]
           ([Name]
           ,[User_Name]
           ,[Mobile]
           ,[Email]
           ,[User_Type]
           ,[User_Password]
           ,[IsActive]
           ,[Address]
           ,[ORG_ID]
,[LastName]


)
     VALUES
           ('" + FirstName +
           "','" + MobileNo +
            "','" + MobileNo +
           "','" + Email +
            "','" + "SurveyUser" +
             "','" + Password +
              "'," + 1 +
               ",'" + Address +
                "',1" + ",'" + LastName + "');";

                Qry = Qry + "SELECT * FROM tbl_UserMaster WHERE User_Name='" + MobileNo + "' AND User_Password='" + Password + "'";
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    models.status = "success";


                    u.UID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    u.FirstName = ds.Tables[0].Rows[0]["Name"].ToString();
                    u.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();

                    u.Email = ds.Tables[0].Rows[0]["EMail"].ToString();
                    u.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    u.MobileNo1 = ds.Tables[0].Rows[0]["Mobile_2"].ToString();
                    u.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    u.Gender = ds.Tables[0].Rows[0]["Mobile"].ToString();

                    u.ImagePath = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                    u.EmailVerifiy = ds.Tables[0].Rows[0]["EMail"].ToString();
                    u.Password = ds.Tables[0].Rows[0]["EMail"].ToString();
                    u.alpha2Code = ds.Tables[0].Rows[0]["EMail"].ToString();
                    models.users = u;
                    models.status = "success";

                    string _OTP = CommonFunction.CreateOTP(6);

                    models.ServerOTP = _OTP;

                    string msg = "Dear User welcome to Mega Survey. Kindly confirm your Mobile No by entering verification code " + _OTP + " in your account. Thank You. Mega Survey";
                    CommonFunction.SendSMS(MobileNo, msg);
                    models.MobileNo = MobileNo;
                    models.IsMobileVerify = "0";

                }
                else
                {
                    models.status = "Fails";
                    models.ErrorMessage = "Somthing went wrong";
                }






            }

            ////////    SqlParameter[] pr = new SqlParameter[]
            ////////    {
            ////////    new SqlParameter("@username", Email),
            ////////    new SqlParameter("@password", Password),
            ////////    new SqlParameter("@flag1", "b"),

            ////////    };
            ////////    SqlDataReader rd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "LoginUser", pr);
            ////////    if (rd.HasRows)
            ////////    {
            ////////        models.status = "Success";

            ////////        rd.Read();
            ////////        // Start Save Data In Session
            ////////        u.UID = Convert.ToInt32(rd["ID"].ToString());
            ////////        u.FirstName = rd["FirstName"].ToString();
            ////////        u.Email = rd["Email"].ToString();
            ////////        u.MobileNo = rd["Mobile"].ToString();
            ////////        u.Gender = rd["Gender"].ToString();
            ////////        u.OTP = Convert.ToInt32(rd["OTP"].ToString());
            ////////        u.ImagePath = rd["PhotoPath"].ToString();
            ////////        u.EmailVerifiy = rd["emailverify"].ToString();
            ////////        u.Password = rd["Password"].ToString();
            ////////        u.alpha2Code = rd["alpha2Code"].ToString();
            ////////        models.users = u;
            ////////        models.status = "Success";
            ////////    }
            ////////    else
            ////////    {
            ////////        models.status = "Fails";
            ////////        models.ErrorMessage = "Invalid Email or Password.";
            ////////    }
            ////////}
            catch (Exception ex)
            {
                ////////    models.status = "Fails";
                ////////    models.ErrorMessage = "Invalid Email or Password.";
                ////////    models.users = u;
            }




            return models;
        }



        public static LoginModels DemoUserRegister(string FirstName, string LastName, string MobileNo1, string MobileNo2, string Email, string Address)
        {

            LoginModels models = new LoginModels();
            users u = new users();


            //if (Convert.ToInt32(GetRecordID("Select count(*) from tbl_UserMaster where Mobile='" + MobileNo + "'")) > 0)
            //{
            //    models.status = "Fails";
            //    models.ErrorMessage = "Mobile no already registerd.";
            //    return models;
            //}


            try
            {





                string Qry = @"INSERT INTO [tbl_DemoUserMaster]
           (
            [FirstName]
            ,[LastName]
           ,[Mobile1]
           ,[Mobile2]
           ,[Email]
           ,[Address]
           ,[CreateDate]
           ,[Exp_Date]
           ,[Allow_Sample]
           ,[User_Type]
           ,[Status]
)
     VALUES
           ('" + FirstName +
           "','" + LastName +
            "','" + MobileNo1 +
           "','" + MobileNo2 +
            "','" + Email +
             "','" + Address +
              "',Getdate()" +
               ",DATEADD(mm, 1, getdate())" +
                ",200" + ",'" + "Demo User" + "',1);";

                Qry = Qry + "SELECT * FROM tbl_DemoUserMaster WHERE Mobile1='" + MobileNo1 + "'";
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    models.status = "success";





                    u.UID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                    models.status = "success";
                    models.IsMobileVerify = "0";

                }

                models.status = "success";
            }


            catch (Exception ex)
            {
                ////////    models.status = "Fails";
                ////////    models.ErrorMessage = "Invalid Email or Password.";
                ////////    models.users = u;
            }




            return models;
        }

        public static LoginModels DemoUserUpdate(string FirstName, string LastName, string MobileNo1, string MobileNo2, string Email, string Address)
        {

            LoginModels models = new LoginModels();
            users u = new users();



            try
            {

                string Qry = @"UPDATE [dbo].[tbl_DemoUserMaster]
        SET [FirstName] = '" + FirstName + "'" +
            ",[LastName] = '" + LastName + "'" +
            ",[Mobile2] = '" + MobileNo2 + "'" +
            ",[Email] = '" + Email + "'" +
           " ,[Address] = '" + Address + "'" +
           "  WHERE  Mobile1='" + MobileNo1 + "'";

                Qry = Qry + " SELECT * FROM tbl_DemoUserMaster WHERE Mobile1='" + MobileNo1 + "'";
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    models.status = "success";


                    u.UID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                    models.status = "success";
                    models.IsMobileVerify = "0";

                }

                models.status = "success";





            }

            catch (Exception ex)
            {
                ////////    models.status = "Fails";
                ////////    models.ErrorMessage = "Invalid Email or Password.";
                ////////    models.users = u;
            }




            return models;
        }




        public static DataSet LoadData(int UserID)
        {
            SqlParameter[] pr = new SqlParameter[]
            {
                new SqlParameter("@UserID",UserID)
            };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_GetUserData", pr);
        }



        public static bool SendEmail(string to, string subject, string body)
        {
            try
            {

                //Encoed
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                string _body = Convert.ToBase64String(bytes);
                //decode

                string base64 = "YWJjZGVmPT0=";
                byte[] bytes1 = Convert.FromBase64String(base64);
                string str = Encoding.UTF8.GetString(bytes1);


                var values = new Dictionary<string, string>
{
            { "FromEmail", System.Configuration.ConfigurationSettings.AppSettings["FEmail"].ToString()},
            { "FromPassword",  System.Configuration.ConfigurationSettings.AppSettings["FPassword"].ToString() },
            { "toEmail", to },
            { "EmailBody",body},
            { "MailSubject", subject},
            { "MailDisplayName", "Mega Survey | Feed Back" },
};

                var content = new System.Net.Http.FormUrlEncodedContent(values);

                var response = client.PostAsync("http://t1.roken4life.com/sendemail.aspx", content);//http://localhost:57867/SendEmail.aspx    http://t1.roken4life.com/sendemail.aspx

                var responseString = response.Result.Content.ReadAsStringAsync();// ReadAsStringAsync();




                return true;
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
                return false;
            }
        }

        public static int ClearSurveyData()
        {
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, "truncate table tbl_SurveyData");

        }


        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }




        public static PartyModels GetPartyDetails(int PartyID)
        {
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_PartyMaster WHERE ID=" + PartyID);

            PartyModels models = new PartyModels();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                models.Party_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                models.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                models.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                models.Short_Name = ds.Tables[0].Rows[0]["Short_Name"].ToString();
                models.Hindi_Name = ds.Tables[0].Rows[0]["HindiName"].ToString();
                models.IconsPath = ds.Tables[0].Rows[0]["IconsName"].ToString();
                models.Type_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Type_ID"].ToString());
            }
            return models;

        }



        public static List<PartyModels> GetPartyList(int Type_ID)
        {
            DataSet ds = new DataSet();

            string Qry = "Select * from tbl_PartyMaster WHERE Type_ID=" + Type_ID;
            if (Type_ID == 0)
            {
                Qry = "Select * from tbl_PartyMaster";
            }

            List<PartyModels> _List = new List<PartyModels>();
            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, Qry);


                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _List.Add(new PartyModels
                        {
                            Party_ID = Convert.ToInt32(dr["ID"].ToString()),
                            Code = dr["Code"].ToString(),
                            Name = dr["Name"].ToString(),
                            Short_Name = dr["Short_Name"].ToString(),
                            Hindi_Name = dr["HindiName"].ToString(),
                            IconsPath = dr["IconsName"].ToString(),
                            Type_ID = Convert.ToInt32(dr["Type_ID"].ToString()),



                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return _List;
        }
        public static List<SurveyMasterModels> GetSurveyList(int ORG_ID)
        {
            DataSet ds = new DataSet();
            List<SurveyMasterModels> _List = new List<SurveyMasterModels>();
            try
            {

                string Qry = @"SELECT SM.ID,SM.[Name] ,SM.[Discreption] ,SM.[Status], ST.Name SurveyType,SF.Survey_For
FROM [tbl_SurveyNameMaster] SM Inner Join tbl_SurveyType ST ON SM.Survey_Type_ID=ST.ID
INNER JOIN  tbl_SurveyFor SF ON SF.ID=SM.Survey_For_ID WHERE SM.ORG_ID=" + ORG_ID;

                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _List.Add(new SurveyMasterModels
                        {
                            SurveyID = Convert.ToInt32(dr["ID"].ToString()),
                            Name = dr["Name"].ToString(),
                            Description = dr["Discreption"].ToString(),
                            SurveyType = dr["SurveyType"].ToString(),
                            SurveyFor = dr["Survey_For"].ToString()
                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return _List;
        }


        public static int SaveParty(PartyModels models)
        {

            try
            {
                SqlParameter[] pr = new SqlParameter[]
                {
                        new SqlParameter("@Name",models.Name),
                        new SqlParameter("@Code",models.Code),
                        new SqlParameter("@Party_ID",models.Party_ID),
                        new SqlParameter("@Short_Name",models.Short_Name),
                        new SqlParameter("@Hindi_Name",models.Hindi_Name),
                        new SqlParameter("@IconsName",models.IconsPath),
                        new SqlParameter("@Type_ID",models.Type_ID),
                        new SqlParameter("@ORG_ID",models.ORG_ID),
                        new SqlParameter("@IsActive",1),
                        new SqlParameter("@Mode",models.Submit_Text),

                };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_SaveUpdateParty", pr);
            }
            catch (Exception ex)
            {

            }

            return 1;
        }

        public static int SaveUpdateSurvey(SurveyMasterModels models)
        {

            try
            {


                string Qry = "INSERT INTO [tbl_SurveyNameMaster] ([Name] ,[Discreption] ,[Status] ,[Survey_For_ID] ,[Survey_Type_ID]) VALUES "
                             + " ( '" + models.Name + "','" + models.Description + "',1," + models.Survey_For_ID + "," + models.Survey_Type_ID + ")";



                SqlParameter[] pr = new SqlParameter[]
                {
                        new SqlParameter("@ORG_ID",models.ORG_ID),
                        new SqlParameter("@SurveyID",models.SurveyID),
                        new SqlParameter("@Name",models.Name),
                        new SqlParameter("@Discreption",models.Description),
                        new SqlParameter("@Survey_For_ID",models.Survey_For_ID),
                        new SqlParameter("@Survey_Type_ID",models.Survey_Type_ID),
                        new SqlParameter("@PartyID",models.PartyIDS),
                        new SqlParameter("@PartyIDS",models.PartyIDS),
                        new SqlParameter("@PersonIDS",models.PersonIDS),
                        new SqlParameter("@Mode",models.Submit_Text),

                };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_CreateSurvey", pr);








                // return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
            }
            catch (Exception ex)
            {

            }

            return 1;
        }


        public static int ChangePartyOrderInSurvey(SurveyMasterModels models)
        {

            try
            {




                SqlParameter[] pr = new SqlParameter[]
                {
                        new SqlParameter("@ORG_ID",models.ORG_ID),
                        new SqlParameter("@Name",models.Name),
                        new SqlParameter("@Discreption",models.Description),

                        new SqlParameter("@PartyIDS",models.PartyIDS),
                         new SqlParameter("@SurveyID",models.SurveyID),

                        new SqlParameter("@OrderIndex",models.OrderIndex),
                        new SqlParameter("@Mode","ChangeOrder"),

                };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_CreateSurvey", pr);








                // return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
            }
            catch (Exception ex)
            {

            }

            return 1;
        }

        public static DataSet GetQuestionReportDS(int Survey_ID)
        {
            SqlParameter[] pr = new SqlParameter[]
               {
                        new SqlParameter("@Survey_ID",Survey_ID),
                       

               };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_GetQuestionReport", pr);



        }





        public static List<SelectListItem> GetStateList()
        {
            List<SelectListItem> _List = new List<SelectListItem>();
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_StateMaster");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _List.Add(new SelectListItem
                    {
                        Text = dr["StateName"].ToString(),
                        Value = dr["ID"].ToString()

                    });
                }
            }

            return _List;


        }
        public static List<SelectListItem> GetLokSabhaList(int StateID)
        {
            List<SelectListItem> _List = new List<SelectListItem>();
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_Loksabha WHERE State_ID=" + StateID);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _List.Add(new SelectListItem
                    {
                        Text = dr["Name"].ToString(),
                        Value = dr["ID"].ToString()

                    });
                }
            }

            return _List;


        }
        public static List<SelectListItem> GetVidhanSabhaList(int LokSabhaID)
        {
            List<SelectListItem> _List = new List<SelectListItem>();
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_Vidhansabha WHERE Loksabha_ID=" + LokSabhaID);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _List.Add(new SelectListItem
                    {
                        Text = dr["NAME"].ToString(),
                        Value = dr["ID"].ToString()

                    });
                }
            }

            return _List;


        }



        public static List<StateModels> GetStateListForView()
        {
            List<StateModels> _List = new List<StateModels>();
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * From tbl_StateMaster");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _List.Add(new StateModels
                    {
                        State_ID = Convert.ToInt32(dr["ID"]),
                        Name = dr["StateName"].ToString(),
                        Code = dr["Code"].ToString(),
                        TTMCSD = dr["TTMCSD"].ToString(),
                    });
                }

            }

            return _List;
        }

        public static List<LoksabhaModels> GetLoksabhaListForView()
        {
            List<LoksabhaModels> _List = new List<LoksabhaModels>();
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "select LM.ID, SM.StateName ,LM.Code,Lm.Name from tbl_Loksabha LM  inner Join tbl_StateMaster SM  ON LM.State_ID=SM.ID");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _List.Add(new LoksabhaModels
                    {
                        Loksabha_ID = Convert.ToInt32(dr["ID"]),
                        State_Name = dr["StateName"].ToString(),
                        Code = dr["Code"].ToString(),
                        Name = dr["Name"].ToString(),
                    });
                }

            }

            return _List;
        }
        public static List<VidhansabhaModels> GetVidhansabhaListForView()
        {
            List<VidhansabhaModels> _List = new List<VidhansabhaModels>();
            DataSet ds = new DataSet();
            string _Qry = @"select M.ID,  SM.StateName,LM.Name Loksabha_Name ,M.Code,M.Name from tbl_Vidhansabha M  inner Join tbl_StateMaster SM  ON M.State_ID=SM.ID 
            INNER JOIN tbl_Loksabha LM ON LM.ID=M.Loksabha_ID ORDER BY SM.StateName,LM.Name,M.Name";
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, _Qry);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _List.Add(new VidhansabhaModels
                    {
                        Vidhansabha_ID = Convert.ToInt32(dr["ID"]),
                        State_Name = dr["StateName"].ToString(),
                        Loksabha_Name = dr["Loksabha_Name"].ToString(),
                        Code = dr["Code"].ToString(),
                        Name = dr["Name"].ToString(),
                    });
                }

            }

            return _List;
        }
        public static List<WardBlockModels> GetWardBlockListForView()
        {
            List<WardBlockModels> _List = new List<WardBlockModels>();
            DataSet ds = new DataSet();
            string _Qry = @"select WB.ID, SM.StateName,LM.Name Loksabha_Name ,WB.Code,M.Name Vidhansabha_Name,WB.Name
             from  tbl_WordBlock WB Inner Join    tbl_Vidhansabha M  ON M.ID=WB.Vidhansabha_ID
             inner Join tbl_StateMaster SM  ON SM.ID=WB.State_ID 
             INNER JOIN tbl_Loksabha LM ON LM.ID=M.Loksabha_ID ORDER BY SM.StateName,LM.Name,M.Name";
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, _Qry);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _List.Add(new WardBlockModels
                    {
                        WardBlock_ID = Convert.ToInt32(dr["ID"]),
                        State_Name = dr["StateName"].ToString(),
                        Loksabha_Name = dr["Loksabha_Name"].ToString(),
                        Vidhansabha_Name = dr["Vidhansabha_Name"].ToString(),
                        Code = dr["Code"].ToString(),
                        Name = dr["Name"].ToString(),
                    });
                }

            }

            return _List;
        }
    }
}