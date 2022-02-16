using System.Text.Json.Serialization;

namespace joyjet_interview_test.Models
{
    public class DeliveryFeeModel
    {
        [JsonPropertyName("eligible_transaction_volume")]
        public EligibleTransactionVolumeModel EligibleTransactionVolume { get; set; }
        public long Price { get; set; }

        public DeliveryFeeModel(EligibleTransactionVolumeModel eligibleTransactionVolume, long price)
        {
            EligibleTransactionVolume = eligibleTransactionVolume;
            Price = price;
        }
    }
}
