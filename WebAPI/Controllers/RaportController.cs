using Application.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using WebAPI.RequestModels;

namespace WebAPI.Controllers
{
    [Authorize(Roles="Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class RaportController : ControllerBase
    {
        private readonly IRaportService _raportService;
        public RaportController(IRaportService raportService)
        {
            _raportService = raportService;
        }

        [SwaggerOperation(Summary="Download file with orders for a given time")]
        [HttpPost]
        public async Task<IActionResult> DownloadExcelFile([FromBody]ExcelDetails model)
        {
            var content =await _raportService.GetDataToExcelDocument(model.StartDate, model.EndDate,model.FileName);
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"{model.FileName}.xlsx";
            return File(content, contentType, fileName);
       
        }           
    }
}
