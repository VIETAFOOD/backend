using AutoMapper;
using BusinessObjects.Dto.Invoices;
using BusinessObjects.Dto.OrderDetail;
using Repositories;
using Services.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Services.Classes
{
    public class BulkInvoiceService : IBulkInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BulkInvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> GetOneInvoice(string key)
        {
            var orderDetail = _unitOfWork.OrderDetailRepository
                                            .Get(filter: x => x.OrderKey.Equals(key),
                                                includeProperties: "ProductKeyNavigation,OrderKeyNavigation.CustomerInfoKeyNavigation");
            var getOneItem = orderDetail.FirstOrDefault();
            var totalPrice = 0;
            var fileName = "";

            if (getOneItem == null)
            {
                return null;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var file = "C:\\JavaCode\\EXE\\backend\\Services\\File\\Order.xlsx";

            using (var package = new ExcelPackage(new FileInfo(file)))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];

                //GET CUSTOMER INFO
                sheet.Cells["C12"].Value = getOneItem.OrderKeyNavigation.CustomerInfoKeyNavigation.Name;
                sheet.Cells["C14"].Value = getOneItem.OrderKeyNavigation.CustomerInfoKeyNavigation.Email;
                sheet.Cells["C16"].Value = getOneItem.OrderKeyNavigation.CustomerInfoKeyNavigation.Address;
                sheet.Cells["C18"].Value = getOneItem.OrderKeyNavigation.CustomerInfoKeyNavigation.Phone;

                //FILL PRODUCT
                foreach (var item in orderDetail)
                {
                    var i = 0;
                    sheet.Cells[i + 24, 2].Value = item.ProductKeyNavigation.Name;
                    sheet.Cells[i + 24, 5].Value = item.ProductKeyNavigation.Weight;
                    sheet.Cells[i + 24, 7].Value = item.Quantity;
                    sheet.Cells[i + 24, 8].Value = item.ActualPrice;
                    sheet.Cells[i + 24, 10].Value = item.ActualPrice * item.Quantity + totalPrice;
                    i++;
                }

                //FILL DATE & TOTAL PRICE
                sheet.Cells["D27"].Value = getOneItem.OrderKeyNavigation.CreatedAt;
                sheet.Cells["D30"].Value = totalPrice;

                fileName = $"Order_{getOneItem.OrderKeyNavigation.CustomerInfoKeyNavigation.Email}" +
                                $"_{getOneItem.OrderKeyNavigation.CreatedAt}.xlsx"
                                .Replace(" ", "_")
                                .Replace(":", "");
                package.Save(fileName);
                return fileName;
            }
        }

    }
}
