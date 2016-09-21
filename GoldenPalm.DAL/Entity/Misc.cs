using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenPalm.DAL
{
    
    [Serializable]
    public class ListColumn
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class ReportParam
    {
        public string ParamName { get; set; }
        public string ParamValue { get; set; }

        public  ReportParam()
        {

        }

        public  ReportParam(string paramName, string paramValue)
        {
            ParamName = paramName;
            ParamValue = paramValue;
        }
    }


    public class CsvColumnAttribute : Attribute
    {
        public bool Export { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }

        public CsvColumnAttribute()
        {
            Export = true;
            Order = int.MaxValue; // so unordered columns are at the end
        }

    }


    public class dtoCredential
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string AppCode { get; set; }
    }


    public class dtoPrincipal
    {
        public string UserSeq { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmpSeq { get; set; }
        public string EmpID { get; set; }
        public string Level1Seq { get; set; }
        public string Level1 { get; set; }
        public string AgencySeq { get; set; }
        public string Agency { get; set; }
        public string DivSeq { get; set; }
        public string Division { get; set; }
        public string SupPosSeq { get; set; }
        public string SupInd { get; set; }
        public string PosSeq { get; set; }
        public string PosNum { get; set; }
        public string UserOrgSeq { get; set; }
        public string Roles { get; set; }
        public int? ReturnCode { get; set; }
        public string ReturnMessage { get; set; }

    }


    public class dtoAppointment
    {
        public string Subject {get;set;}
        public string Body {get;set;}
        public DateTime Start {get;set;}
        public DateTime End {get;set;}
        public string Location {get;set;}
        public string RequiredAttendess {get;set;}
        public TimeSpan Duration { get; set; }
    }





}
