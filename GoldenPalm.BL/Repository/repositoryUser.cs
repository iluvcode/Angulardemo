using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GoldenPalm.DAL;

namespace GoldenPalm.BL
{
    public class repositoryUser:GenericRepository<dtoNominee>
    {

        public List<dtoUser> GetAllUsers(object UserName, string RowID = null)
        {
            param.Clear();

            List<dtoUser> dto = dal.getDVProc("GOLDENPALM.Pkg_COMMON.sp_GetEmployees").ToTable().AsEnumerable().Select(row => new dtoUser
            {
                UserName = Convert.ToString(row["EMPNAME"]),
                LastName = Convert.ToString(row["LastName"]),
              FirstName=Convert.ToString(row["FirstName"]),
                UserID = Convert.ToString(row["USER_ID"]),
                id = Convert.ToString(row["USER_SEQ"]),
                text = Convert.ToString(row["User"]),
                UserSeq = Convert.ToString(row["USER_SEQ"]),
                EmpSeq = Convert.ToString(row["EMPLOYEE_SEQ"]),
                Phone = Convert.ToString(row["EMPPHONE"]),
                Email = Convert.ToString(row["EMAIL"]),
              
                Agency = Convert.ToString(row["AGENCY"]),
                AgencySeq = Convert.ToString(row["AGENCY_SEQ"]),
                RowID = RowID,
            }).ToList();
            return dto;
        }
    }
}
