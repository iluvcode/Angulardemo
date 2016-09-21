using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenPalm.DAL
{
    public static class Codes
    {

        public static List<CodeMasterDTO> listCodeMaster { get; set; }
        public static List<CodeDetailDTO> listCode { get; set; }

        public static bool refreshCodes { get; set; }


        public static string getField(string Code, string CodeMaster, int FieldNum = 0)
        {

            var tmpList = listCode.SingleOrDefault(x => x.CodeDetail == Code & x.CodeMaster == CodeMaster);
            return getFiledValue(tmpList, FieldNum);

        }


        public static string getField(int? Code, string CodeMaster, int FieldNum = 0)
        {
            if (Code != null)
            {
                var tmpList = listCode.SingleOrDefault(x => x.CodeDetailSeq == Code & x.CodeMaster == CodeMaster);
                return getFiledValue(tmpList, FieldNum);
            }
            else
                return "";
        }


        public static string getFieldByText(string Code, string CodeMaster, int FieldNum = 0)
        {

            var tmpList = listCode.SingleOrDefault(x => x.CodeDetailText == Code & x.CodeMaster == CodeMaster);
            return getFiledValue(tmpList, FieldNum);
        }


        public static int getCodeSeqByText(string Text, string CodeMaster)
        {
            return listCode.Where(x => x.CodeDetailText == Text & x.CodeMaster == CodeMaster).Select(x => x.CodeDetailSeq).SingleOrDefault();
        }

        public static int getCodeSeq(string Code, string CodeMaster)
        {
            return listCode.Where(x => x.CodeDetail == Code & x.CodeMaster == CodeMaster).Select(x => x.CodeDetailSeq).SingleOrDefault();
        }




        private static string getFiledValue(CodeDetailDTO tmpList, int FieldNum)
        {
            string retValue = "";

            if (tmpList != null)
            {

                switch (FieldNum)
                {
                    case 0:
                        retValue = tmpList.CodeDetailText;
                        break;
                    case 1:
                        retValue = tmpList.Field1;
                        break;
                    case 2:
                        retValue = tmpList.Field2;
                        break;
                    case 3:
                        retValue = tmpList.Field3;
                        break;
                    case 4:
                        retValue = tmpList.Field4;
                        break;
                }
            }

            return retValue;
        }



        public static List<CodeDetailDTO> GetAllCode(string CodeMaster)
        {

            return listCode.Where(x => x.CodeMaster == CodeMaster).ToList();
        }

        public static List<CodeDetailDTO> GetAllCode(string CodeMaster, int Level1Seq)
        {

            return listCode.Where(x => x.CodeMaster == CodeMaster & x.Level1Seq == Convert.ToString(Level1Seq)).ToList();
        }

        public static List<CodeDetailDTO> GetAllCode(string CodeMaster, string RefCodeDetail)
        {

            return listCode.Where(x => x.CodeMaster == CodeMaster & x.Field1 == RefCodeDetail).ToList();
        }

    


    }
}
