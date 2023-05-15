using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSurvey.Models
{
    public class StateModels
    {
        public int ORG_ID { get; set; }
        public int State_ID { get; set; }
        public string Code { get; set; }
        public string TTMCSD { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<StateModels> _List = new List<StateModels>();
    }

    public class DistrictModels
    {
        public int ORG_ID { get; set; }
        public string Code { get; set; }
        public int State_ID { get; set; }
        public string State_Name { get; set; }
        public int District_ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<DistrictModels> _List = new List<DistrictModels>();
    }
    public class LoksabhaModels
    {
        public int ORG_ID { get; set; }
        public int State_ID { get; set; }
        public string Code { get; set; }
        public string State_Name { get; set; }
        public int District_ID { get; set; }
        public int District_Name { get; set; }
        public int Loksabha_ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<LoksabhaModels> _List = new List<LoksabhaModels>();
    }
    public class VidhansabhaModels
    {
        public int ORG_ID { get; set; }
        public string Code { get; set; }
        public int State_ID { get; set; }
        public string State_Name { get; set; }
        public int District_ID { get; set; }
        public int District_Name { get; set; }
        public int Loksabha_ID { get; set; }
        public string Loksabha_Name { get; set; }
        public int Vidhansabha_ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<VidhansabhaModels> _List = new List<VidhansabhaModels>();
    }


    public class WardBlockModels
    {
        public int ORG_ID { get; set; }
        public string Code { get; set; }
        public int State_ID { get; set; }
        public string State_Name { get; set; }
        public int District_ID { get; set; }
        public int District_Name { get; set; }
        public int Loksabha_ID { get; set; }
        public string Loksabha_Name { get; set; }
        public int Vidhansabha_ID { get; set; }
        public string Vidhansabha_Name { get; set; }
        public int WardBlock_ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<WardBlockModels> _List = new List<WardBlockModels>();
    }



    public class CityModels
    {
        public int ORG_ID { get; set; }
        public string Code { get; set; }
        public int State_ID { get; set; }
        public string State_Name { get; set; }
        public int District_ID { get; set; }
        public int District_Name { get; set; }
        public int Loksabha_ID { get; set; }
        public string Loksabha_Name { get; set; }
        public int Vidhansabha_ID { get; set; }
        public int Vidhansabha_Name { get; set; }
        public int WardBlock_ID { get; set; }
        public int WardBlock_Name { get; set; }
        public int City_ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<WardBlockModels> _List = new List<WardBlockModels>();
    }


    public class BoothModels
    {
        public string Code { get; set; }
        public int ORG_ID { get; set; }
        public int State_ID { get; set; }
        public string State_Name { get; set; }
        public int District_ID { get; set; }
        public int District_Name { get; set; }
        public int Loksabha_ID { get; set; }
        public string Loksabha_Name { get; set; }
        public int Vidhansabha_ID { get; set; }
        public int Vidhansabha_Name { get; set; }
        public int WardBlock_ID { get; set; }
        public int WardBlock_Name { get; set; }
        public int City_ID { get; set; }
        public string  City_Name { get; set; }
        public int Boot_ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<WardBlockModels> _List = new List<WardBlockModels>();
    }
}