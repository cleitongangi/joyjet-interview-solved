using System.Text.Json.Serialization;

namespace joyjet_interview_test.Models
{
    public class EligibleTransactionVolumeModel
    {
        [JsonPropertyName("min_price")]
        public long MinPrice { get; set; }
        [JsonPropertyName("max_price")]
        public long? MaxPrice { get; set; }

        public EligibleTransactionVolumeModel(long minPrice, long? maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}
