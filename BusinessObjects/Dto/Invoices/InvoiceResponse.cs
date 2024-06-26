using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Invoices
{
    public class InvoiceResponse
    {
        public string OrderDetailKey { get; set; }
        public int Quantity { get; set; }
        public float ActualPrice { get; set; }
        public ProductResponse Product { get; set; }
        public CustomerInformationResponse CutomerInfo { get; set; }
    }
}
