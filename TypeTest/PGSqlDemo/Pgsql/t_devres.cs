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
    
    public partial class t_devres
    {
        public string id { get; set; }
        public string resname { get; set; }
        public string rescode { get; set; }
        public Nullable<int> restypeid { get; set; }
        public string restype { get; set; }
        public Nullable<int> restypeinfoid { get; set; }
        public string restypeinfo { get; set; }
        public Nullable<System.DateTime> regtime { get; set; }
        public string regper { get; set; }
        public string stationid { get; set; }
        public string station { get; set; }
        public string ip { get; set; }
        public Nullable<int> port { get; set; }
        public string resposition { get; set; }
        public Nullable<int> localport { get; set; }
        public string ftpip { get; set; }
        public Nullable<int> ftpport { get; set; }
        public string ftpuser { get; set; }
        public string ftppasswd { get; set; }
        public string antenname { get; set; }
        public Nullable<short> antenidex { get; set; }
        public Nullable<short> addr485 { get; set; }
        public Nullable<short> status { get; set; }
    }
}
