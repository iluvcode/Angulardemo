using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenPalm.BL
{

    public static class Settings
    {

        public static string AppBase { get; set; }
        public static string AppCode { get; set; }
        public static string Module { get; set; }
        public static string ReportBase { get; set; }
        
        public static string AppName
        {
            get
            {
                return getAppName(Module);
            }

        }

        public static string ShowError { get; set; }
        public static string UrlAuth { get; set; }
        public static int DefaultPageSize { get; set; }


        public static int[] PageSizeDropdown { get; set; }

        static Settings()
        {
            PageSizeDropdown = new int[] { 10, 20, 50, 100, 200 };
            DefaultPageSize = 100;
        }


        public static string getAppName(string Module)
        {

            
                return "Golden Palm Awards";

       

        }




     

    }
}
