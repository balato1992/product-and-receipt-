using System;
using System.Data.SqlClient;

namespace product_and_receipt.Models.DBs.Tables
{
    public class DBInfoTable : SqlHelper
    {
        public static string TABLE => "DB_INFO";
        public static string FIELD_DATA => "DATA";


        public DBInfoTable(string connectionString, LogFunc log = null) : base(connectionString, log)
        {
        }

        public DBVersion GetVersion()
        {
            string sql = $"IF (EXISTS "
                + $" (SELECT * FROM INFORMATION_SCHEMA.TABLES "
                + $" WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{TABLE}')) "
                + $" BEGIN "
                + $" SELECT * FROM {TABLE} ORDER BY {FIELD_DATA} DESC "
                + $" END ";

            DBVersion version = DBVersion.v1;
            DoReadAll(sql,
                (SqlDataReader reader) =>
                {
                    ConvertToEnum(reader[FIELD_DATA], out version);
                });

            return version;
        }
    }

    public enum DBVersion { v1, v2 }
}
