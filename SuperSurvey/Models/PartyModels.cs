using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace SuperSurvey.Models
{
    public class PartyModels
    {

        public string PanelTitle { get; set; }
        public int Party_ID { get; set; }

        [Required(ErrorMessage = "Please Enter Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Remote("CheckDuplicatePartyName", "Common", ErrorMessage = "Name already exists.", AdditionalFields = "ORG_ID,Party_ID,Type_ID")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Short Name")]
        public string Short_Name { get; set; }
        [Required(ErrorMessage = "Please Enter Regional Language")]
        public string Hindi_Name { get; set; }
        public string IconsPath { get; set; }
        public int Type_ID { get; set; }
        private string _SubmitText = "Save";
        public int ORG_ID { get; set; }
        public string Submit_Text { get { return _SubmitText; } set { _SubmitText = value; } }
        public List<PartyModels> PartyList = new List<PartyModels>();
    }


    public class SurveyAssingModels
    {

        public string UserIDS { get; set; }
        public int SurveyID { get; set; }
        public int ORG_ID { get; set; }
        public List<SelectListItem> SurveyList = new List<SelectListItem>();

        [Required(ErrorMessage = "Please Select State")]
        public int StateID { get; set; }
        public List<SelectListItem> StateList = new List<SelectListItem>();

        public int? Loksabha_ID { get; set; }
        public List<SelectListItem> LoksabhaList = new List<SelectListItem>();


        public int? Vidhansabha_ID { get; set; }

        public int? WardBlock_ID { get; set; }


        public List<SelectListItem> VidhansabhaList = new List<SelectListItem>();

        public List<UserRegisterModels> UserList { get; set; }

        [Required(ErrorMessage = "Please Enter Exp Date")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/(([1-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/(([1-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/(([1-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Please enter valid date in dd/mm/yyyy.")]
        public string ExpDate { get; set; }
        [Required(ErrorMessage = "Please Enter Max Limit")]
        [Range(1000, 10000, ErrorMessage = "Please Enter Max Limit Between 1000-10000")]
        public string MaxLimit { get; set; }

    }



    public class UserRegisterModels
    {
        public bool IsChecked { get; set; }
        public int ORG_ID { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        [Remote("CheckDuplicateUserName", "Common", ErrorMessage = "User Name already exists.", AdditionalFields = "ORG_ID,UserID")]
        public string User_Name { get; set; }

        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address")]
        [Required(ErrorMessage = "Please Enter Email")]
        [Remote("CheckDuplicateEmail", "Common", ErrorMessage = "Email already exists.", AdditionalFields = "ORG_ID,UserID")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile No")]
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        public List<UserRegisterModels> _UserList = new List<UserRegisterModels>();

    }



    public class IconsModels
    {
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public List<IconsModels> _List = new List<IconsModels>();
    }


    public class NameCountModels
    {
        public int ID { get; set; }
        public int? PID { get; set; }
        public string PName { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public List<NameCountModels> _List = new List<NameCountModels>();
    }

    public class NameCountModels1
    {
        public int ID { get; set; }
        public int? PID { get; set; }
        public string PName { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public int MID { get; set; }
       
    }


    public class QuestionReportModels
    {
        public int SurveyID { get; set; }
        public int? QuestionID { get; set; }
        public List<SelectListItem> SurveyList = new List<SelectListItem>();
        public List<SelectListItem> QuestionListForSelection = new List<SelectListItem>();

        public List<QuestionListModes> QuestionList = new List<QuestionListModes>();

    }
    public class QuestionListModes
    {
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public List<QuestionResponseModel> OptionList = new List<QuestionResponseModel>();
    }
    public class QuestionResponseModel
    {
        public int QuestionID { get; set; }
        public string OptionText { get; set; }
        public string OptionCount { get; set; }
    }

    public class SurveyFinelReport
    {
        public int SurveyID { get; set; }
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/(([1-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/(([1-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/(([1-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Please enter valid date in dd/mm/yyyy.")]
        public string FDate { get; set; }
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/(([1-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/(([1-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/(([1-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Please enter valid date in dd/mm/yyyy.")]
        public string TDate { get; set; }
        public string SerachFieldsName { get; set; }
        public string SerachFieldsValue { get; set; }
        public int TotalCount { get; set; }

        public List<SelectListItem> SurveyList = new List<SelectListItem>();
        public List<CheckListBoxModels> GroupList { get; set; }
    }


}