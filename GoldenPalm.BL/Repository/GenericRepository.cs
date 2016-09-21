using GoldenPalm.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace GoldenPalm.BL
{
    public abstract class GenericRepository<T> : IRepository<T>
    {

        public GoldenPalm.DAL.DAL dal;

        public string _appCode;
        public string _appBase;
        public string _pwd;
        public string _ConStr;


        protected GenericRepository()
        {

            _appCode = Settings.AppCode.ToLower();
            //_appCode = Settings.AppCode.ToLower();
            _appBase = Settings.AppBase.ToLower();

            //_appBase = "wqa";

            _pwd = Security.Security_Encryption(_appCode, "PBCSECURITY", _appBase);

            _pwd = "goldenpalm";

            _ConStr = "Data Source=" + _appBase + ";User Id=" + _appCode + ";Password=" + _pwd + ";";
            dal = new GoldenPalm.DAL.DAL(_ConStr);

            // Load all the codes
            //if (Codes.listCode == null || Codes.refreshCodes)
            //{
            //    Codes.listCodeMaster = GetAllCodeMaster();
            //    Codes.listCode = GetAllCodeDetail();
            //    Codes.refreshCodes = false;
            //}

        }


        protected GenericRepository(string MatView)
        {

            _appCode = Settings.AppCode.ToLower() + "_connect";
            _appCode = Settings.AppCode.ToLower();
            _appBase = Settings.AppBase.ToLower();

            //_appBase = "wqa";

            //_pwd = Security.Security_Encryption(_appCode, "PBCSECURITY", _appBase);

            _pwd = "goldenpalm";

            _ConStr = "Data Source=" + _appBase + ";User Id=" + _appCode + ";Password=" + _pwd + ";";
            dal = new GoldenPalm.DAL.DAL(_ConStr);

        }


        public virtual List<T> GetAll()
        {
            return new List<T>();
        }

        public virtual T GetById(object Id)
        {
            T t = default(T);
            return t;
        }

        public virtual ReturnType Save(T t)
        {
            return new ReturnType();
        }

        public virtual List<ListColumn> getListByID(object Id)
        {
            return new List<ListColumn>();
        }

        public virtual List<T> getEntityListByID(object Id)
        {
            return new List<T>();
        }

        public List<DBParams> param = new List<DBParams>();



        protected void changeAppType(string AppType)
        {
            if (AppType == "SEC")
            {
                //_appBase = Settings.SecAppBase.ToLower();
                _pwd = Security.Security_Encryption("eccs_connect", "PBCSECURITY", _appBase);

                _ConStr = "Data Source=" + _appBase + ";User Id=eccs_connect;Password='" + _pwd + "';";

                dal = new GoldenPalm.DAL.DAL(_ConStr);
            }

            else if (AppType == "TFS")
            {
                //_appBase = Settings.SecAppBase.ToLower();
                _appCode = "harauto_connect";
                _pwd = "first";


                _ConStr = "Data Source=" + _appBase + ";User Id=" + _appCode + ";Password=" + _pwd + ";";

                dal = new GoldenPalm.DAL.DAL(new DbSettings(_ConStr, "retCursor"));

            }


        }


        public List<ListColumn> getSelectList(DataView dv, string value1, string text1)
        {
            List<ListColumn> lc = new List<ListColumn>();

            lc = dv.Table.AsEnumerable().Select(row => new ListColumn
            {
                value = Convert.ToString(row[value1]),
                text = Convert.ToString(row[text1])

            }).ToList();

            return lc;
        }
      



        public List<ListColumn> getSelectListEncrypt(DataView dv, string value1, string text1)
        {
            List<ListColumn> lc = new List<ListColumn>();

            lc = dv.Table.AsEnumerable().Select(row => new ListColumn
            {
                value = GoldenPalm.DAL.UtilWeb.Encrypt(Convert.ToString(row[value1])),
                text = Convert.ToString(row[text1])

            }).ToList();

            return lc;
        }

        public List<ListColumn> getDropDownList(string ProcName, string value, string text)
        {
            return getSelectList(dal.getDVProc(ProcName), value, text);
        }


        public List<ListColumn> getDropDownListEncrypt(string ProcName, string value, string text)
        {
            return getSelectListEncrypt(dal.getDVProc(ProcName), value, text);
        }
        //public List<CodeMasterDTO> GetAllCodeMaster()
        //{

        //    param.Clear();

        //    List<CodeMasterDTO> dto = dal.getDVProc("PTS_WEB.Pkg_CODE.get_code_master").ToTable().AsEnumerable().Select(row => new CodeMasterDTO
        //    {
        //        Text = Convert.ToString(row["CODE_MASTER_TEXT"]),
        //        Code = Convert.ToString(row["CODE_MASTER"]),
        //        Desc = Convert.ToString(row["CODE_MASTER_DESC"]),
        //        Level1Seq = Convert.ToString(row["Level1_Seq"])


        //    }).ToList();

        //    return dto;
        //}


        //public List<CodeDetailDTO> GetAllCodeDetail()
        //{
        //    List<CodeDetailDTO> dto = dal.getDVProc("PTS_WEB.Pkg_CODE.get_code_detail").ToTable().AsEnumerable().Select(row => new CodeDetailDTO
        //    {
        //        CodeDetailSeq = Convert.ToInt32(row["CODE_DETAIL_SEQ"]),
        //        CodeDetailText = Convert.ToString(row["CODE_DETAIL_TEXT"]),
        //        CodeMaster = Convert.ToString(row["CODE_MASTER"]),
        //        CodeDetail = Convert.ToString(row["CODE_DETAIL"]),
        //        CodeDetailDesc = Convert.ToString(row["CODE_DETAIL_DESC"]),
        //        Field1 = Convert.ToString(row["Field1"]),
        //        Field2 = Convert.ToString(row["Field2"]),
        //        Field3 = Convert.ToString(row["Field3"]),
        //        Field4 = Convert.ToString(row["Field4"]),
        //        Level1Seq = Convert.ToString(row["Level1_Seq"])
        //    }).OrderBy(x => x.CodeDetailText).ToList();
        //    return dto;
        //}


       
        //Convert List to datatable
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


    }
}
