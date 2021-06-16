using Application.DtoModels.OrderLineModels;
using System.Collections.Generic;


namespace Application.DtoModels.OrderModels
{
    public class CreateOrderDto
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public IEnumerable<CreateOrderLineDto> Line { get; set; }
    }
}
