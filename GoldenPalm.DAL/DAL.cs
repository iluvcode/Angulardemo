using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;


namespace GoldenPalm.DAL
{


    public class DAL: IDisposable
    {


        //Connection Properties

        private string _OracleConnectionString;
        private DbSettings DBSetings;

        private OracleConnection conn;

        private OracleCommand cmd;




        public DAL(string OracleConnectionString)
        {
            _OracleConnectionString = OracleConnectionString;
            DBSetings = new DbSettings();

        }

        public DAL(DbSettings dbsettings)
        {
            DBSetings = dbsettings;
            _OracleConnectionString = DBSetings.ConnectionString;
           
        }


        private void setConnection()
        {
            if (conn == null)
            {
                conn = new OracleConnection();
                conn.ConnectionString = _OracleConnectionString;
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }
        


        private OracleCommand SetCommand()
        {

            setConnection();
            return conn.CreateCommand();

        }

        private OracleCommand SetCommand(string sqlString)
        {
            cmd = SetCommand();

            cmd.CommandText = sqlString;

            return cmd;
        }

        public void AddParameters(List<DBParams> arrParam)
        {

            cmd.Parameters.Clear();

            if (arrParam != null)
            {

                foreach (var param in arrParam)
                {
                    OracleParameter idbParameter;


                    idbParameter = new OracleParameter(param.ParamName, formatForDB(Convert.ToString(param.ParamValue)));
                    idbParameter.Size = param.size;

                    idbParameter.Direction = (ParameterDirection)param.ParamDirection;

                    

                    idbParameter.OracleType = param.typeOracle;

                    cmd.Parameters.Add(idbParameter);

                }
            }
        }

        public void AddParameters(string Imgparam, Byte[] Image, List<DBParams> arrParam)
        {

            cmd.Parameters.Clear();

            if (arrParam != null)
            {

                for (int i = 0; i < arrParam.Count; i++)
                {

                    DbParameter idbParameter = new OracleParameter(arrParam[i].ParamName, formatForDB(Convert.ToString(arrParam[i].ParamValue)));

                    idbParameter.Direction = (ParameterDirection)arrParam[i].ParamDirection;
                    cmd.Parameters.Add(idbParameter);

                }
            }
            DbParameter idbParameter1 = new OracleParameter(Imgparam, Image);

            cmd.Parameters.Add(idbParameter1);
        }






        public DataSet getDataSet_withImage(string sqlString, CommandType cmdType, string ImgParam, Byte[] Image, List<DBParams> arrParam)
        {
            cmd = SetCommand(sqlString);
            cmd.CommandType = cmdType;

            OracleDataAdapter dataAdapter = new OracleDataAdapter();

            AddParameters(ImgParam, Image, arrParam);

            dataAdapter.SelectCommand = cmd;

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            cmd.Connection.Close();
            //Dispose();
            return dataSet;
        }

        public DataSet getDataSet(string sqlString, CommandType cmdType, List<DBParams> arrParam)
        {
            cmd = SetCommand(sqlString);
            cmd.CommandType = cmdType;

            OracleDataAdapter dataAdapter = new OracleDataAdapter();

            AddParameters(arrParam);

            cmd.Parameters.Add(new OracleParameter(DBSetings.CursorName, OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, DBNull.Value));

            dataAdapter.SelectCommand = cmd;

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            foreach (DBParams item in arrParam)
            {
                if (item.ParamDirection == ParameterDirection.Output)
                    item.ParamValue = cmd.Parameters[item.ParamName].Value;
            }

            cmd.Connection.Close();
            //Dispose();
            return dataSet;
        }


        public DataSet getDataSetCursor(string sqlString,string cursorName,CommandType cmdType, List<DBParams> arrParam)
        {
            cmd = SetCommand(sqlString);
            cmd.CommandType = cmdType;

            OracleDataAdapter dataAdapter = new OracleDataAdapter();

            AddParameters(arrParam);

            cmd.Parameters.Add(new OracleParameter(cursorName, OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, DBNull.Value));

            dataAdapter.SelectCommand = cmd;

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            foreach (DBParams item in arrParam)
            {
                if (item.ParamDirection == ParameterDirection.Output)
                    item.ParamValue = cmd.Parameters[item.ParamName].Value;
            }

            cmd.Connection.Close();
            //Dispose();
            return dataSet;
        }

                

        public DataSet getDataSetMultiple(string sqlString, CommandType cmdType, List<DBParams> arrParam)
        {
            cmd = SetCommand(sqlString);
            cmd.CommandType = cmdType;

            OracleDataAdapter dataAdapter = new OracleDataAdapter();

            AddParameters(arrParam);

            cmd.Parameters.Add(new OracleParameter("retcursor1 ", OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, DBNull.Value));

            cmd.Parameters.Add(new OracleParameter("retcursor2", OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, DBNull.Value));

            cmd.Parameters.Add(new OracleParameter("retcursor3", OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, DBNull.Value));

            cmd.Parameters.Add(new OracleParameter("retcursor4", OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, DBNull.Value));

            cmd.Parameters.Add(new OracleParameter("retcursor5", OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, DBNull.Value));
            dataAdapter.SelectCommand = cmd;

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            foreach (DBParams item in arrParam)
            {
                if (item.ParamDirection == ParameterDirection.Output)
                    item.ParamValue = cmd.Parameters[item.ParamName].Value;
            }

            cmd.Connection.Close();
            //Dispose();
            return dataSet;
        }

        

        public DataView getDV(string sqlString, List<DBParams> arrParam)
        {
            return getDataSet(sqlString, CommandType.Text, arrParam).Tables[0].DefaultView;
        }

        public DataView getDV(string sqlString)
        {
            return getDataSet(sqlString, CommandType.Text, null).Tables[0].DefaultView;
        }

        public int execSQL(string sqlString, List<DBParams> arrParam)
        {

            cmd = SetCommand(sqlString);

            try
            {

                AddParameters(arrParam);

                return cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conn.Close();
            }

        }


        public IReturnType execProc(string sqlString, List<DBParams> arrParam, bool useStandard = true)
        {
            List<DBParams> standardParams = new List<DBParams>();
            if (useStandard)
            {
                standardParams.Add(new DBParams("intreturnCode", "I"));
                standardParams.Add(new DBParams("strReturnMsg", "V"));
            }
            foreach (DBParams db in standardParams)
                arrParam.Add(db);
            cmd = SetCommand(sqlString);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                
                AddParameters(arrParam);
                IReturnType rt = new DALReturnType();

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["intreturnCode"].Value.ToString() != "")
                    rt.ReturnCode = (int?)cmd.Parameters["intreturnCode"].Value;
                rt.ReturnMessage = cmd.Parameters["strReturnMsg"].Value.ToString();
                return rt;
            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conn.Close();
            }

        }



        public IReturnType execBlob(string sqlString, List<DBParams> arrParam, byte[] Image, string ImageName = "arg_file",  bool useStandard = true)
        {
            List<DBParams> standardParams = new List<DBParams>();
            if (useStandard)
            {
                standardParams.Add(new DBParams("intreturnCode", "I"));
                standardParams.Add(new DBParams("strReturnMsg", "V"));
            }
            foreach (DBParams db in standardParams)
                arrParam.Add(db);

            
            cmd = SetCommand();

            OracleTransaction tx = cmd.Connection.BeginTransaction();
            cmd.Transaction = tx;

            StringBuilder strCommand = new StringBuilder("declare xx blob; begin dbms_lob.createtemporary(xx, false, 0);");

            strCommand.Append(" :tempblob1:= xx;");

            cmd.Parameters.Add(new OracleParameter("tempblob1", OracleType.Blob)).Direction = ParameterDirection.Output;

            strCommand.Append(" end;");

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = strCommand.ToString();

            cmd.ExecuteNonQuery();
            

            OracleLob blob = cmd.Parameters["tempblob1"].Value as OracleLob;

            blob.Write(Image, 0, Image.Length);

            cmd.Parameters.Clear();

              cmd.CommandText = sqlString;
              cmd.CommandType = CommandType.StoredProcedure;

            AddParameters(arrParam);
            cmd.Parameters.Add(ImageName, OracleType.Blob).Value = blob;
            IReturnType rt = new DALReturnType();

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["intreturnCode"].Value.ToString() != "")
                rt.ReturnCode = (int?)cmd.Parameters["intreturnCode"].Value;
            rt.ReturnMessage = cmd.Parameters["strReturnMsg"].Value.ToString();

            tx.Commit();

            conn.Close();
  
            return rt;


        }



        private String execScalarProc(string sqlString, List<DBParams> arrParam)
        {

            cmd = SetCommand(sqlString);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                AddParameters(arrParam);

                object result;
                result = cmd.ExecuteScalar();

                if (result == null)
                    return String.Empty;
                else
                    return result.ToString();
            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conn.Close();
            }

        }

        public String execScalarProc(string sqlString, params string[] pParams)
        {

            return execScalarProc(sqlString, createDBParams(pParams));

        }

        public String execScalarProc2(string sqlString, List<DBParams> arrParam)
        {

            return execScalarProc(sqlString, arrParam);

        }

        public String execScalar(string sqlString, List<DBParams> arrParam)
        {

            cmd = SetCommand(sqlString);

            try
            {

                AddParameters(arrParam);

                object result;
                result = cmd.ExecuteScalar();

                if (result == null)
                    return String.Empty;
                else
                    return result.ToString();
            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conn.Close();
            }

        }

        public object formatForDB(string str)
        {
            //string tmpStr = str.Replace("'", "''");
            string tmpStr = str.Trim();

            if (tmpStr == string.Empty)
                return System.DBNull.Value;
            else
                return tmpStr;
        }

        public DataView getDV(string sqlString, params string[] pParams)
        {
            return getDataSet(sqlString, CommandType.Text, createDBParams(pParams)).Tables[0].DefaultView;
        }

        public DataView getDVProc(string sqlString, params string[] pParams)
        {
            return getDataSet(sqlString, CommandType.StoredProcedure, createDBParams(pParams)).Tables[0].DefaultView;
        }

        public DataView getDVProcCursor(string sqlString, string cursorName, List<DBParams> pParams)
        {

            return getDataSetCursor(sqlString,cursorName, CommandType.StoredProcedure, pParams).Tables[0].DefaultView;
        }

        public DataView getDVProc_withImage(string sqlString, string ImgParam, Byte[] Image, params string[] pParams)
        {
            return getDataSet_withImage(sqlString, CommandType.StoredProcedure, ImgParam, Image, createDBParams(pParams)).Tables[0].DefaultView;
        }

        public DataView getDVProc(string sqlString, List<DBParams> pParams)
        {
            return getDataSet(sqlString, CommandType.StoredProcedure, pParams).Tables[0].DefaultView;
        }

        public DataSet getDatasetProc(string sqlString, params string[] pParams)
        {
            return getDataSet(sqlString, CommandType.StoredProcedure, createDBParams(pParams));
        }


        public List<DBParams> createDBParams(params string[] pParams)
        {
            List<DBParams> clsParam = new List<DBParams>();

            for (int i = 0; i < pParams.Length-1; i++)
            {
                clsParam.Add(new DBParams(pParams[i], pParams[i + 1], "V"));
                i = i + 1;
            }
            return clsParam;
        }

        public void Dispose()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Dispose();

        }

    }
}
