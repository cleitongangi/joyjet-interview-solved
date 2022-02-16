using joyjet_interview_test.Interfaces.Factories.DiscountType;

namespace joyjet_interview_test.Factories.DiscountType
{
    public class DiscountByAmount : IDiscountType, IDiscountByAmount
    {
        public long GetDiscountedPrice(long discountValue, long price)
        {
            var result = price - discountValue;
            if (result < 0)
                return 0;

            return result;
        }
    }
}
