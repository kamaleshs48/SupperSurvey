using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSurvey.Models
{
    public class SelectModels
    {
        public int PID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public List<SelectModels> GList = new List<SelectModels>();

    }
    public class LoginModels
    {
        public string status { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public users users { get; set; }
        public string ErrorMessage { get; set; }
        public string IsMobileVerify { get; set; }
        public string ServerOTP { get; set; }

        public string MobileNo { get; set; }


    }
    public class users
    {

        public int UID { get; set; }
        public string UserName { get; set; }
        public int OTP { get; set; }
        public string EmailVerifiy { get; set; }
        public string ImagePath { get; set; }
        public string Male { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string MobileNo1 { get; set; }
        public string Gender { get; set; }
        public string BirthYear { get; set; }
        public string Email { get; set; }
        public string AboutYou { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string BusinessName { get; set; }
        public string fbuserID { get; set; }
        public string gluserID { get; set; }
        public string CountryName { get; set; }
        public string alpha2Code { get; set; }
    }


    public class SignUpModels
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string UID { get; set; }
        public string UName { get; set; }
        public string Message { get; set; }
        public users users { get; set; }
        public string MobileNo { get; set; }
        public string Sex { get; set; }

    }
    public class ProfileModels
    {
        public string Base64Image { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UID { get; set; }
        public string MobileNo { get; set; }
        public string OEmail { get; set; }
        public string AboutUS { get; set; }
        public string PAN { get; set; }
        public string FaceBook { get; set; }
        public string LinkedIn { get; set; }
        public string Verification { get; set; }
    }
}