using Application.DtoModels.OrderLineModels;
using System;
using System.Collections.Generic;


namespace Application.DtoModels.OrderModels
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public DateTime DateCreated { get; set; }
        public List<OrderLineDto> Line { get; set; }
    }
}
