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
    
    public partial class t_stationtask
    {
        public string id { get; set; }
        public string taskname { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public string createper { get; set; }
        public Nullable<short> tasktype { get; set; }
        public Nullable<System.DateTime> begintime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public Nullable<int> taskinterval { get; set; }
        public Nullable<short> timetpye { get; set; }
        public Nullable<short> taskstatus { get; set; }
        public string raskcomment { get; set; }
        public string statuscomment { get; set; }
        public string stationid { get; set; }
    }
}