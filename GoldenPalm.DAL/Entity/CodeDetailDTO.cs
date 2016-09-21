using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenPalm.DAL
{

    public class CodeDetailDTO : AggregateRoot
    {
        public int CodeDetailSeq { get; set; }
        public string CodeDetail { get; set; }
        public string CodeDetailText { get; set; }
        public string CodeDetailDesc { get; set; }
        public string CodeMaster { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string ActiveInd { get; set; }
        public string Level1Seq { get; set; }
        
    }


}
