using joyjet_interview_test.Interfaces.Factories.DiscountType;

namespace joyjet_interview_test.Factories.DiscountType
{
    public class DiscountByPercentage : IDiscountType, IDiscountByPercentage
    {
        public long GetDiscountedPrice(long discountValue, long price)
        {
            if (discountValue > 100)
                return 0;

            var discount = (discountValue * price) / 100.0;
            return (long)Math.Floor(price - discount);
        }
    }
}
