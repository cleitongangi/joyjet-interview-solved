namespace joyjet_interview_test.Interfaces.Factories.DiscountType
{
    public interface IDiscountType
    {
        long GetDiscountedPrice(long discountValue, long price);
    }
}
