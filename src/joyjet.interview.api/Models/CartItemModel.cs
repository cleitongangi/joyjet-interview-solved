using System.Text.Json.Serialization;

namespace joyjet_interview_test.Models
{
    public class CartItemModel
    {
        [JsonPropertyName("article_id")]
        public int ArticleId { get; set; }
        public int Quantity { get; set; }

        public CartItemModel(int articleId, int quantity)
        {
            ArticleId = articleId;
            Quantity = quantity;
        }
    }
}
