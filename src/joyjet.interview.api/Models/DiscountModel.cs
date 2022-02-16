using joyjet_interview_test.Enums;
using System.Text.Json.Serialization;

namespace joyjet_interview_test.Models
{
    public class DiscountModel
    {
        [JsonPropertyName("article_id")]
        public int ArticleId { get; set; }
        public DiscountTypeEnum Type { get; set; }
        public long Value { get; set; }

        public DiscountModel(int articleId, DiscountTypeEnum type, long value)
        {
            ArticleId = articleId;
            Type = type;
            Value = value;
        }
    }
}
