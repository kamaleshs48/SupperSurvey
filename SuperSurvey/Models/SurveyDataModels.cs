using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSurvey.Models
{
    public class SurveyDataModels
    {
        public string RowID { get; set; }
        public string Party_ID { get; set; }
        public string Survey_ID { get; set; }
        public string State_ID { get; set; }
        public string LokSabha_ID { get; set; }
        public string VidhanSabha_ID { get; set; }
        public string Word_ID { get; set; }
        public string Booth_ID { get; set; }
        public string Area_ID { get; set; }
        public string OtherArea { get; set; }
        public string GPSCoordinate { get; set; }
        public string SurveyDate { get; set; }
        public string UserID { get; set; }
        public string PersonID { get; set; }
        public string Street { get; set; }


        public string Gender_ID { get; set; }
        public string Age_Group_ID { get; set; }
        public string Religion_ID { get; set; }
        public string Caste_ID { get; set; }
        public string Education_ID { get; set; }
        public string Profession_ID { get; set; }


        public string Email { get; set; }
        public string Name { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string QResponse1 { get; set; }
        public string QResponse2 { get; set; }
        public string QResponse3 { get; set; }
        public string QResponse4 { get; set; }
        public string QResponse5 { get; set; }
        public string QResponse6 { get; set; }
        public string QResponse7 { get; set; }
        public string QResponse8 { get; set; }
        public string QResponse9 { get; set; }
        public string QResponse10 { get; set; }
        public string QResponse11 { get; set; }
        public string QResponse12 { get; set; }
        public string QResponse13 { get; set; }
        public string QResponse14 { get; set; }
        public string QResponse15 { get; set; }
        public string QResponse16 { get; set; }
        public string QResponse17 { get; set; }
        public string QResponse18 { get; set; }
        public string QResponse19 { get; set; }
        public string QResponse20 { get; set; }
        public string QResponse21 { get; set; }
        public string QResponse22 { get; set; }
        public string QResponse23 { get; set; }
        public string QResponse24 { get; set; }
        public string QResponse25 { get; set; }
        public string QResponse26 { get; set; }
        public string QResponse27 { get; set; }
        public string QResponse28 { get; set; }
        public string QResponse29 { get; set; }
        public string QResponse30 { get; set; }

        public List<MQResponse> MQRList { get; set; }
        public List<SurveyDataModels> SLit = new List<SurveyDataModels>();
    }

    public class MQResponse
    {
        public string Question_ID { get; set; }
        public string SN { get; set; }
        public string QResponse { get; set; }
        public string Survey_ID { get; set; }


    }



    public class UserProfileModels
    {




        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string password { get; set; }
        public string UID { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public string AlternateMobileNo { get; set; }
    }
}