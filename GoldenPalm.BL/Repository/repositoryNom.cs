using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GoldenPalm.DAL;

namespace GoldenPalm.BL
{
    public class repositoryNom : GenericRepository<dtoNomination>
    {

        public List<dtoNomination> getNominations(string YearSeq="",string NominationSeq="")
        {
            param.Clear();

            DataView dv = new DataView();
            DataTable dt = new DataTable();
            if(!string.IsNullOrEmpty(YearSeq))
            param.Add(new GoldenPalm.DAL.DBParams("arg_year_seq", YearSeq, "I"));
            if (!string.IsNullOrEmpty(NominationSeq))
            param.Add(new GoldenPalm.DAL.DBParams("arg_nomination_seq", NominationSeq, "I"));
            dt = dal.getDVProc("GOLDENPALM.PKG_NOMINATION.sp_getNominations", param).ToTable();

            List<dtoNomination> dto = dt.AsEnumerable().Select(row => new dtoNomination
            {
                NominationSeq = Convert.ToInt32(row["NOMINATION_SEQ"]),
                Nominees = Convert.ToString(row["Nominees"]),
                NominationNum = Convert.ToString(row["NOMINATION_NUM"]),
                AgencySeq = Convert.ToInt32(row["AGENCY_SEQ"]),
                DeptName = Convert.ToString(row["DEPT_NAME"]),
                YearSeq = Convert.ToInt32(row["FISCAL_YEAR"]),
                NomAmount = Convert.IsDBNull(row["AMOUNT"]) ? 0 : Convert.ToDouble(row["AMOUNT"]),
                Desc = Convert.ToString(row["DESCRIPTION"]),
                Reason = Convert.ToString(row["REASON"]),
                SubmittedBy = Convert.ToString(row["SUBMITTED_BY"]),
                WinInd = Convert.ToString(row["WINNING_IND"]),
                Title = Convert.ToString(row["TITLE"])
            }).ToList();

            return dto;
        }

        public ReturnType SaveNominations(dtoNomination dtoNom)
        {
            param.Clear();
            param.Add(new DBParams("arg_year_seq", dtoNom.YearSeq, "I"));
            param.Add(new DBParams("arg_Agency_seq", dtoNom.AgencySeq, "I"));
            param.Add(new DBParams("arg_title", dtoNom.Title, "V"));
            param.Add(new DBParams("arg_description", dtoNom.Desc , "V"));
            param.Add(new DBParams("arg_submitted_by", dtoNom.SubmittedBy , "V"));
            param.Add(new DBParams("arg_wining_ind", dtoNom.WinInd, "V"));
            param.Add(new DBParams("arg_amount", dtoNom.NomAmount, "I"));
            param.Add(new DBParams("arg_reason", dtoNom.Reason, "V"));
            param.Add(new DBParams("arg_userid", "test", "V"));
            return new ReturnType(dal.execProc("PKG_NOMINATION.sp_SaveNominations", param));
        }

        public List<dtoHistory> GetHistory(int Arg_Seq, string TableName, string ColName)
        {
            DataTable dt = new DataTable();
            param.Clear();
            param.Add(new DBParams("arg_seq", Arg_Seq, "I"));
            param.Add(new DBParams("arg_table_name", TableName, "V"));
            param.Add(new DBParams("arg_col_name", ColName, "V"));
            dt = dal.getDVProc("PKG_CHANGE_LOGS.sp_getHistotyLogs", param).ToTable();

            List<dtoHistory> dtohistory = dt.AsEnumerable().Select(row => new dtoHistory
                {
                    value = Convert.ToString(row["CHANGE"]),
                    userId = Convert.ToString(row["USER"]),
                    DateEnt = Convert.ToDateTime(row["DATE"]),
                    Cname = ColName
                }).ToList();
            return dtohistory;
        }

        public List<ListColumn> getDeptList()
        {
            return getSelectList(dal.getDVProc("GOLDENPALM.PKG_COMMON.sp_getDepartments"), "Agency_seq", "Agency_name");
        }
    }
}
