using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace SuperSurvey.Models
{
    public class SurveyMasterModels
    {
        public int SurveyID { get; set; }
        public int ORG_ID { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Select Survey For")]
        public int Survey_For_ID { get; set; }
        public List<SelectListItem> Survey_For_List = new List<SelectListItem>();
        [Required(ErrorMessage = "Please Select Survey Type")]
        public int Survey_Type_ID { get; set; }


        public string SurveyFor { get; set; }
        public string SurveyType { get; set; }
        private string _SubmitText = "Submit";
        public string Submit_Text { get { return _SubmitText; } set { _SubmitText = value; } }
        public List<SurveyMasterModels> _SurveyList = new List<SurveyMasterModels>();


        public string PartyIDS { get; set; }
        public string PersonIDS { get; set; }
        public string OrderIndex { get; set; }

        public List<CheckListBoxModels> PartyList { get; set; }
        public List<CheckListBoxModels> PersoneList { get; set; }
        public List<CheckListBoxModels> PartyPersoneList { get; set; }






    }


    public class CheckListBoxModels
    {


        public int Type_ID { get; set; }
        public string Value
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
        public bool Checked
        {
            get;
            set;
        }

        public string Order_Index { get; set; }

        public string GroupName { get; set; }
    }



    public class ReportDataModels
    {
        public Int32 PageCount { get; set; }
        public string RowNumber { get; set; }
        public string User_Name { get; set; }
        public string Survey_Name { get; set; }
        public string Party_Name { get; set; }
        public string StateName { get; set; }
        public string LokSabhaName { get; set; }

        public string Vidhansabha_Name { get; set; }
        public string Ward_Block { get; set; }
        public string Area { get; set; }
        public string OtherArea { get; set; }
        public string GPSCoordinate { get; set; }
        public string Survey_Date { get; set; }
        public string Post_Date { get; set; }
        public string Persone_ID { get; set; }
        public string Street { get; set; }

    }
}