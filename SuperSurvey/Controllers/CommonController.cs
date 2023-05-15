using SuperSurvey.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperSurvey.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult CheckDuplicatePartyName(string Name, string ORG_ID, int Type_ID, int Party_ID)
        {

            int a = Convert.ToInt32(CommonFunction.GetRecordID("Select count(id) from tbl_PartyMaster WHERE Name='" + Name.Trim() + "'  AND ORG_ID=" + ORG_ID + " AND ID <> " + Party_ID + " AND Type_ID =" + Type_ID));
            if (a > 0)
            {
                return Json("Name already exists.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckDuplicateEmail(string Email, string ORG_ID, int UserID)
        {

            int a = Convert.ToInt32(CommonFunction.GetRecordID(" Select count(id) from tbl_UserMaster WHERE Email='" + Email.Trim() + "'  AND ORG_ID=" + ORG_ID + " AND ID <> " + UserID));
            if (a > 0)
            {
                return Json("Email already exists.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }





        }

        public ActionResult CheckDuplicateUserName(string User_Name, string ORG_ID, int UserID)
        {

            int a = Convert.ToInt32(CommonFunction.GetRecordID(" Select count(id) from tbl_UserMaster WHERE User_Name='" + User_Name.Trim() + "'  AND ORG_ID=" + ORG_ID + " AND ID <> " + UserID));
            if (a > 0)
            {
                return Json("Name already exists.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }





        }

    }
}