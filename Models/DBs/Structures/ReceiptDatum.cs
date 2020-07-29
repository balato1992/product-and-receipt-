using System;
using System.Collections.Generic;

namespace product_and_receipt.Models.DBs.Structures
{
    public class ReceiptDatum
    {
        public string Id { get; set; }
        public string Payee { get; set; }
        public string ReceiptDate { get; set; }

        public List<ReceiptItemDatum> Items { get; set; }

        public ReceiptDatum(string id, string payee, string receiptDate)
        {
            Id = id;
            Payee = payee;
            ReceiptDate = receiptDate;
            Items = new List<ReceiptItemDatum>();
        }

        public void AddItem(ReceiptItemDatum item)
        {
            Items.Add(item);
        }
    }
}
