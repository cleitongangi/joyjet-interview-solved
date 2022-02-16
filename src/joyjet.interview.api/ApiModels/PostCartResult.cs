using System.Text.Json.Serialization;

namespace joyjet_interview_test.ApiModels
{
    public class PostCartResult
    {
        public int Id { get; set; }
        public long Total => SubTotal + DeliveryFee;

        [JsonIgnore]
        public long SubTotal { get; set; }
        [JsonIgnore]
        public long DeliveryFee { get; set; }

        public PostCartResult(int id, long subTotal)
        {
            Id = id;            
            SubTotal = subTotal;
        }
    }
}
