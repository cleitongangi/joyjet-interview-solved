using joyjet_interview_test.Models;
using System.Text.Json.Serialization;

namespace joyjet_interview_test.ApiModels
{
    public class PostCartInput
    {
        public IEnumerable<ArticleModel> Articles { get; set; }
        public IEnumerable<CartModel> Carts { get; set; }
        [JsonPropertyName("delivery_fees")]
        public IEnumerable<DeliveryFeeModel> DeliveryFees { get; set; }
        public IEnumerable<DiscountModel> Discounts { get; set; }

        public PostCartInput(IEnumerable<ArticleModel> articles, IEnumerable<CartModel> carts, IEnumerable<DeliveryFeeModel> deliveryFees, IEnumerable<DiscountModel> discounts)
        {
            Articles = articles;
            Carts = carts;
            DeliveryFees = deliveryFees;
            Discounts = discounts;
        }
    }
}
