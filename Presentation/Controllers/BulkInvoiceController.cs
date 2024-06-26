using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/bulk-invoice")]
    public class BulkInvoiceController : ControllerBase
    {
        private readonly IBulkInvoiceService _bulkInvoiceService;
        public BulkInvoiceController(IBulkInvoiceService bulkInvoiceService)
        {
            _bulkInvoiceService = bulkInvoiceService;
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> InvoiceDownload(string key) {
            var getOne = await _bulkInvoiceService.GetOneInvoice(key);
            return Ok(new {fileName = getOne});
        }
    }
}
