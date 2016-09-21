using System.Web;

namespace GoldenPalm.BL
{
    public static class SessionHandler
    {
        private static void SetSession<T>(string sessionId, T value)
        {
            HttpContext.Current.Session[sessionId] = value;
        }

        private static T GetSession<T>(string sessionId)
        {
            return (T)HttpContext.Current.Session[sessionId];
        }

    

        public static string ASCSUrl
        {
            get
            {
                return GetSession<string>("ASCS_URL");
            }
            set
            {
                SetSession<string>("ASCS_URL", value);
            }
        }


    }
}
