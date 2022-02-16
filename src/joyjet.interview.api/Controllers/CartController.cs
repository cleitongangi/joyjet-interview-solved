using joyjet_interview_test.ApiModels;
using joyjet_interview_test.Interfaces.Services;
using joyjet_interview_test.Services;
using Microsoft.AspNetCore.Mvc;

namespace joyjet_interview_test.Controllers
{
    [ApiController]
    [Route("API")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        [Route("Cart")]
        [HttpPost]
        public IActionResult PostCart([FromBody] PostCartInput input)
        {
            var result = _cartService.CalculateCart(input);
            return Ok(new { carts = result });
        }
    }
}