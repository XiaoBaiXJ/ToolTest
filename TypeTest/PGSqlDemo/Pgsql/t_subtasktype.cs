//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PGSqlDemo.Pgsql
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_subtasktype
    {
        public int id { get; set; }
        public Nullable<int> tasktypeid { get; set; }
        public string tasktype { get; set; }
        public string subtasktype { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> edittime { get; set; }
        public string editper { get; set; }
    }
}
