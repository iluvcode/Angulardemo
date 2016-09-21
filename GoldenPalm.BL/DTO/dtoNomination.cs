using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GoldenPalm.BL
{
   public class dtoNomination
    {
       public string WinInd { get; set; }
       public bool WinIndVm { get; set; }
       public int NominationSeq { get; set; }

       [Required(ErrorMessage = "Department Required")]
       public int AgencySeq { get; set; }

       public string DeptName { get; set; }

       [DisplayFormat(DataFormatString = "{0:F2}")]
       [DataType(DataType.Currency)]
       public double NomAmount { get; set; }

       [Required(ErrorMessage = "Year Required")]
       public int YearSeq { get; set; }
   
       [Required(ErrorMessage = "Title Required")]
       public string Title { get; set; }

       [Required(ErrorMessage = "Description Required")]
       public string Desc { get; set; }

       public string Reason { get; set; }

       [Required(ErrorMessage = "Submit Name Required")]
       public string SubmittedBy { get; set; }
       public string StatusSeq { get; set; }
       public string Status { get; set; }

       public string Nominees { get; set; }
       public string NominationNum { get; set; }
    }
}
