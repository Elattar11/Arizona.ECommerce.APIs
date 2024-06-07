using Arizona.APIs.DTOs;
using Arizona.APIs.Errors;
using Arizona.Core.Entities.OrderAggregate;
using Arizona.Core.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arizona.APIs.Controllers
{
    
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var mappedAddress = _mapper.Map<OrderAddressDto, OrderAddress>(orderDto.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, mappedAddress);

            if (order is null) return BadRequest(new ApiResponse(404));

            return Ok(_mapper.Map<Order , OrderToReturnDto>(order));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string buyerEmail)
        {
            var orders = await _orderService.GetOrderForUserAsync(buyerEmail); 

            return Ok(_mapper.Map<IReadOnlyList<Order> , IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [ProducesResponseType(typeof(OrderToReturnDto) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id, string buyerEmail)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(buyerEmail, id);

            if (order is null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        


    }
}
