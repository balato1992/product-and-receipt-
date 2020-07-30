using System;
using System.Data.SqlClient;
using System.IO;

namespace product_and_receipt.Models.DBs.Tables
{
    public class DBInfoTable : SqlHelper
    {
        public static string TABLE => "DB_INFO";
        public static string FIELD_NAME => "NAME";
        public static string FIELD_VALUE => "VALUE";


        public static string NAME_VERSION => "VERSION";

        public DBInfoTable(string connectionString, LogFunc log = null) : base(connectionString, log)
        {
        }

        public DBVersion GetVersion()
        {
            string sql = $"IF (EXISTS "
                + $" (SELECT * FROM INFORMATION_SCHEMA.TABLES "
                + $" WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{TABLE}')) "
                + $" BEGIN "
                + $" SELECT * FROM {TABLE} WHERE {FIELD_NAME}='{NAME_VERSION}' "
                + $" END ";

            DBVersion version = DBVersion.v1;
            DoReadAll(sql,
                (SqlDataReader reader) =>
                {
                    ConvertToEnum(reader[FIELD_VALUE], out version);
                });

            return version;
        }


        /*
            BACKUP DATABASE [DBNAME] 
            TO DISK = N'PATH' WITH NOFORMAT, 
            NOINIT, NAME = N'Chad Database Backup', SKIP,NOREWIND, NOUNLOAD, STATS = 10

            RESTORE VERIFYONLY FROM  DISK = N'PATH' WITH FILE= 1, NOUNLOAD, NOREWIND

            RESTORE DATABASE [DBNAME] FROM  DISK = N'PATH' WITH FILE = 1, NOUNLOAD, STATS = 10
             */
        public void Backup(string root)
        {
            using var connection = new SqlConnection(ConnectionString);

            string dbName = connection.Database;
            string rootPath = $"{root}\\backup";
            string fileName = $"{rootPath}\\Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

            if(!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            string sql = $" BACKUP DATABASE [{dbName}]  "
                + $" TO DISK = N'{fileName}' WITH NOFORMAT, "
                + $" NOINIT, NAME = N'Database Backup({dbName} )', SKIP, NOREWIND, NOUNLOAD, STATS = 10 ";

            DoExecuteNonQuery_NoTrans(sql);
        }
    }

    public enum DBVersion { v1, v2 }
}
