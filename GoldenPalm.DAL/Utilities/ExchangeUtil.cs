using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using System.Web;

namespace GoldenPalm.DAL
{
    public class ExchangeUtil
    {
        ExchangeService service;

        public ExchangeUtil()
        {
            service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            
            service.Credentials = new WebCredentials("unatani", "mactian25", "pbcgov");

            service.Url = new Uri("https://webmail.pbcgov.org/ews/exchange.asmx");
        }


         public ExchangeUtil(string UserName, string Password, string Domain = "pbcgov")
        {
            service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            
            service.Credentials = new WebCredentials(UserName, Password, Domain);

            service.Url = new Uri("https://webmail.pbcgov.org/ews/exchange.asmx");
        }

        public string CreateAppointment(dtoAppointment appt)
        {
            Appointment appointment = new Appointment(service);
            appointment.Subject = appt.Subject;
            appointment.Body = appt.Body;
            appointment.Start = appt.Start;
            appointment.End = appt.End;
            appointment.Location = appt.Location;

            var array = appt.RequiredAttendess.Split(',');
            foreach (var arr in array)
            {
                appointment.RequiredAttendees.Add(arr);
            }
            try
            {
                appointment.Save(SendInvitationsMode.SendOnlyToAll);
                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }

    }

  
}
