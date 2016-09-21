using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPalm.BL
{
    public class dtoNominee
    {
        public int NomineeSeq { get; set; }

        public int EmployeeSeq { get; set; }
        public int NominationSeq { get; set; }
        public double AwardAmount { get; set; }
        public string PlaqueInfo { get; set; }
        public string RSVPInd { get; set; }
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmployeeID{ get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string EmployeeType { get; set; }
        public string Email { get; set; }
        public string Agency { get; set; }
        public string DeptSeq { get; set; }
        public string ContPerCentage { get; set; }
        public dtoUser UserInfo { get; set; }

        public dtoNominee()
        {
            UserInfo = new dtoUser();
          
        }
    }
}
