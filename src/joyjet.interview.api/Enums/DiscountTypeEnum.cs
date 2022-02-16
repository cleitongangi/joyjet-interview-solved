using System.Text.Json.Serialization;

namespace joyjet_interview_test.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DiscountTypeEnum
    {
        Amount,
        Percentage
    }
}
