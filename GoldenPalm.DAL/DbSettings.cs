using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Data.OracleClient;
using System.Data;
using System.Data.Common;

namespace GoldenPalm.DAL
{
    public class DbSettings
    {
        public string ConnectionString { get; set; }
        public string CursorName { get; set; }

        public DbSettings()
        {
            CursorName = "ocur_data";
        }

        public DbSettings(string connectionString, string cursorName)
        {
            ConnectionString = connectionString;
            CursorName = cursorName;
        }
    }



    public class DBParams
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }
        public ParameterDirection ParamDirection { get; set; }


        public OracleType typeOracle { get; set; }
        public int size { get; set; }

        public DBParams(string ParamName, string type)
        {
            this.ParamName = ParamName;
            this.ParamValue = null;
            this.ParamDirection = ParameterDirection.Output;
            this.typeOracle = getParamType(type);
            if (type == "C")
                this.size = 0;
            else
                this.size = 4000;
        }



        public DBParams(string ParamName, object ParamValue, string type, int size=4000)
        {
            this.ParamName = ParamName;
            this.ParamValue = ParamValue;
            this.ParamDirection = ParameterDirection.Input;
            this.typeOracle = getParamType(type);

            if (type == "D")
                this.size = 0;
            else
                this.size = size;
        }


        private OracleType getParamType(string strType)
        {
            OracleType varType = new OracleType();
            switch (strType)
            {
                case "V":
                    varType = OracleType.NVarChar;
                    break;
                case "C":
                    varType = OracleType.Cursor;
                    break;
                case "N":
                    varType = OracleType.Number;
                    break;
                case "I":
                    varType = OracleType.Int32;
                    break;
                case "D":
                    varType = OracleType.DateTime;
                    break;
                case "B":
                    varType = OracleType.Blob;
                    break;
                case "SI":
                    varType = OracleType.Int16;
                    break;
                case "DC":
                    varType = OracleType.Float;
                    break;
                case "DB":
                    varType = OracleType.Double;
                    break;
                case "CL":
                    varType = OracleType.Clob;

                    break;
            }
            return varType;
        }
    }



    public interface IReturnType
    {

        string ReturnMessage { get; set; }
        int? ReturnCode { get; set; }
    }


    public class DALReturnType : IReturnType
    {

        public string ReturnMessage { get; set; }
        public int? ReturnCode { get; set; }
    }

}
