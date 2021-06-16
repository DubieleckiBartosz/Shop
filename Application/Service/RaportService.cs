using Application.DtoModels.OrderModels;
using Application.Exceptions;
using Application.Service.Interfaces;
using AutoMapper;
using ClosedXML.Excel;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Service
{
    public class RaportService : IRaportService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<RaportService> _logger;
        public RaportService(IMapper mapper,IOrderRepository orderRepository,ILogger<RaportService> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<byte[]> GetDataToExcelDocument(DateTime startDate, DateTime endDate,string name)
        {
            if (endDate > DateTime.Now) throw new BadRequestException("The end date cannot be greater than today");
            if (endDate.Date == DateTime.Today) endDate=DateTime.Now;
            var ordersDb = await _orderRepository.GetOrderByDateAsync(startDate, endDate);
            if (ordersDb is null) throw new NotFoundException($"Not found orders between {startDate} and {endDate}");
            _logger.LogInformation($"Raport from {startDate} to {endDate}");
    
            var orders = _mapper.Map<List<OrderDto>>(ordersDb);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(name);
                worksheet.Cell(1, 1).Value = "City";
                worksheet.Cell(1, 2).Value = "Postal Code";
                worksheet.Cell(1, 3).Value = "Street";
                worksheet.Cell(1, 4).Value = "Date";
                worksheet.Cell(1, 5).Value = "Amount";

                worksheet.RangeUsed().AddConditionalFormat().WhenBetween("=A1", "=E1")
                    .Fill.SetBackgroundColor(XLColor.LightGreen);

                for (int i = 1; i <= orders.Count; i++)
                {
                    worksheet.Cell(i + 1, 1).Value =
                        orders[i - 1].City;
                    worksheet.Cell(i + 1, 2).Value =
                        orders[i - 1].PostalCode;
                    worksheet.Cell(i + 1, 3).Value =
                        orders[i - 1].Street;
                    worksheet.Cell(i + 1, 4).Value =
                        orders[i - 1].DateCreated.ToString();
                    worksheet.Cell(i + 1, 5).Value =
                        orders[i - 1].Amount;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }
    }
}
