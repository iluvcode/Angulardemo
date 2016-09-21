using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoldenPalm.DAL;

namespace GoldenPalm.BL
{
    [Serializable]
    public class ReturnType
    {
        public string ReturnMessage { get; set; }
        public int? ReturnCode { get; set; }

        public ReturnType()
        {
        }

        public ReturnType(IReturnType rt)
        {
            ReturnMessage = rt.ReturnMessage;
            ReturnCode = rt.ReturnCode;
        }

        public ReturnType(int returnCode, string returnMessage)
        {
            ReturnMessage = returnMessage;
            ReturnCode = returnCode;
        }


    }

  
}
