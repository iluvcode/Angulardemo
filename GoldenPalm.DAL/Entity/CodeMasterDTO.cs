using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenPalm.DAL
{
    public class CodeMasterDTO : AggregateRoot
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public string Desc { get; set; }
        public string Type { get; set; }
        public string Level1Seq { get; set; }
        public string FieldIndexs { get; set; }
        public string FieldNames { get; set; }
        public string FieldDesc { get; set; }
        public string FieldReferences { get; set; }
        public List<MasterFieldsDTO> Fields { get; set; }
        
    }
    public class MasterFieldsDTO : AggregateRoot
    {
        public string FieldIndex { get; set; }
        public string FieldName { get; set; }
        public string FieldDesc { get; set; }
        public string FieldReference { get; set; }
        public string FieldValue { get; set; }
        
    }

}
