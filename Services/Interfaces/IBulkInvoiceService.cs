using BusinessObjects.Dto.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBulkInvoiceService
    {
        Task<string> GetOneInvoice(string key);
    }
}
