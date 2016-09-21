using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.IO;

namespace GoldenPalm.DAL
{
    public class Security
    {
        public static string Security_Encryption(string strSource, string strSystemKey, string strGlobalKey)
        {

            string lstrSrc = null;
            string lstrSys = null;
            string lstrEncrypted = null;
            string lstrGlb = null;
            string flgUC = null;
            int i = 0;
            int j = 0;
            int k = 0;
            int tempAsc = 0;
            byte[] lastrSysKeys = null;
            byte[] lastrGlbKeys = null;

            lstrSrc = strSource;
            lstrSys = strSystemKey;
            if (Strings.InStr(1, strGlobalKey, "uc:") > 0)
            {
                flgUC = "Y";
                lstrGlb = Strings.Replace(strGlobalKey, "uc:", "");
            }
            else
            {
                flgUC = "N";
                lstrGlb = strGlobalKey;
            }
            lastrSysKeys = new byte[Strings.Len(lstrSys) + 1];
            lastrGlbKeys = new byte[Strings.Len(lstrGlb) + 1];

            for (i = 1; i <= Strings.Len(lstrSys); i++)
            {
                lastrSysKeys[i] = Convert.ToByte(Strings.Asc(Strings.Mid(lstrSys, i, 1)));
            }

            for (i = 1; i <= Strings.Len(lstrGlb); i++)
            {
                lastrGlbKeys[i] = Convert.ToByte(Strings.Asc(Strings.Mid(lstrGlb, i, 1)));
            }


            j = 1;
            k = 1;
            for (i = 1; i <= Strings.Len(lstrSrc); i++)
            {
                tempAsc = Strings.Asc(Strings.Mid(lstrSrc, i, 1)) ^ lastrSysKeys[j] ^ lastrGlbKeys[k];
                if (tempAsc < 32 & flgUC == "Y")
                {
                    tempAsc = tempAsc + 32;
                }
                lstrEncrypted = lstrEncrypted + Strings.Chr(tempAsc);
                if (j == Strings.Len(lstrSys))
                    j = 0;
                if (k == Strings.Len(lstrGlb))
                    k = 0;
                j = j + 1;
                k = k + 1;
            }

            dynamic lstrEncrypted2 = null;
            dynamic li_cnt = null;
            for (li_cnt = 1; li_cnt <= Strings.Len(lstrEncrypted); li_cnt++)
            {
                if (Strings.Asc(Strings.Mid(lstrEncrypted, li_cnt, 1)) == 0)
                {
                    lstrEncrypted2 = lstrEncrypted2 + Strings.Chr(129);
                }
                else
                {
                    lstrEncrypted2 = lstrEncrypted2 + Strings.Mid(lstrEncrypted, li_cnt, 1);
                }
            }
            lstrEncrypted = Strings.Replace(lstrEncrypted2, "\"", "QT");

            return AddSingleQuotes(lstrEncrypted2);


        }

        public static string AddSingleQuotes(string strPassword)
        {

            string tmpPassword = null;
            tmpPassword = strPassword.Replace("'", "");
            return tmpPassword;
        }


    }
}
