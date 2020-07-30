using product_and_receipt.Models.DBs;
using System;

namespace product_and_receipt.Models
{
    public class GlobalInstance
    {
        internal static DBHelper DB { get; private set; }
        internal static string ContentRoot { get; private set; }

        internal static void Initialize(string contentRoot, string connectionString)
        {
            ContentRoot = contentRoot;

            DB = new DBHelper(connectionString, (Exception ex, string msgPrefix) =>
            {
                DB.AppLogTable.InsertException(msgPrefix, ex);
            });


            var timer = new CustomTimer("Check backup time", 30 * 60 * 1000, () =>
            {
                bool b = DB.DBInfoTable.CheckIfNeedBackup();

                if (b)
                {
                    DB.DBInfoTable.Backup(ContentRoot);
                    DB.RecordTable.Insert("Auto Backup DB Done", null);
                }
            });
            timer.StartTimer();
        }
    }
}
