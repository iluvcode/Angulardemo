using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenPalm.DAL;

namespace GoldenPalm.BL
{
   public class repositoryNominee : GenericRepository<dtoNominee>
    {
        public List<dtoNominee> getNominee(string NomineeSeq="",string NominationSeq="")
        {
            param.Clear();

            DataView dv = new DataView();
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(NomineeSeq))
            param.Add(new GoldenPalm.DAL.DBParams("arg_nominee_seq", NomineeSeq, "I"));

            if (!string.IsNullOrEmpty(NominationSeq))
                param.Add(new GoldenPalm.DAL.DBParams("arg_nomination_seq", NominationSeq, "I")); 

            dt = dal.getDVProc("GOLDENPALM.PKG_NOMINEE.sp_getNominee", param).ToTable();
            List<dtoNominee> Nominee_dto = dt.AsEnumerable().Select(row => new dtoNominee
            {
                NominationSeq = Convert.ToInt32(row["NOMINATION_SEQ"]),
                EmployeeSeq = Convert.ToInt32(row["EMPLOYEE_SEQ"]),
                PlaqueInfo = Convert.ToString(row["PLAQUE_INFO"]),
                AwardAmount = Convert.ToDouble(row["AWARD_AMOUNT"]),
                RSVPInd = Convert.ToString(row["ATTEND_IND"]),
                EmployeeName = Convert.ToString(row["FIRST_NAME"]) +' '+ Convert.ToString(row["LAST_NAME"]),
                Department = Convert.ToString(row["ORGANIZATION_NAME"]),

                ContPerCentage = Convert.ToString(row["CONTRIBUTION_PERCENTAGE"])

            }).ToList();

            return Nominee_dto;
        }

        public ReturnType SaveNominee(dtoNominee Nominee)
        {

            param.Clear();

            param.Add(new GoldenPalm.DAL.DBParams("arg_nominee_seq", Convert.ToInt32(Nominee.NomineeSeq), "I"));
            param.Add(new GoldenPalm.DAL.DBParams("arg_nomination_seq", Nominee.NominationSeq, "I"));
            param.Add(new GoldenPalm.DAL.DBParams("arg_employee_seq",Nominee.EmployeeSeq, "I"));
            param.Add(new GoldenPalm.DAL.DBParams("arg_first_name", Nominee.FirstName, "V"));
               param.Add(new GoldenPalm.DAL.DBParams("arg_middle_name", Nominee.MiddleName, "V"));
                  param.Add(new GoldenPalm.DAL.DBParams("arg_last_name", Nominee.LastName, "V"));
                  param.Add(new GoldenPalm.DAL.DBParams("arg_dept_name", Nominee.Agency, "V"));
                  param.Add(new GoldenPalm.DAL.DBParams("arg_dept_seq", Nominee.DeptSeq, "I"));
                  param.Add(new GoldenPalm.DAL.DBParams("arg_job_title", Nominee.JobTitle, "V"));
                  param.Add(new GoldenPalm.DAL.DBParams("arg_plaque_info", Nominee.PlaqueInfo, "V"));
                  param.Add(new GoldenPalm.DAL.DBParams("arg_award_amount", Nominee.AwardAmount, "V"));
                  param.Add(new GoldenPalm.DAL.DBParams("arg_rsvp_ind", Nominee.RSVPInd, "V"));
                  param.Add(new GoldenPalm.DAL.DBParams("arg_email", Nominee.Email, "V"));

            param.Add(new GoldenPalm.DAL.DBParams("arg_user_seq", 1, "I"));

            return new ReturnType(dal.execProc("GOLDENPALM.Pkg_Nominee.sp_saveNominee", param));


        }
    }
}
