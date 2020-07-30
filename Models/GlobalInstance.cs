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
        }
    }
}
