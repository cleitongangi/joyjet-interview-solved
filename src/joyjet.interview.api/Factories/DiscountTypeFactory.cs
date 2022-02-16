using joyjet_interview_test.Enums;
using joyjet_interview_test.Interfaces.Factories;
using joyjet_interview_test.Interfaces.Factories.DiscountType;

namespace joyjet_interview_test.Factories
{
    public class DiscountTypeFactory : IDiscountTypeFactory
    {
        private readonly IDiscountByAmount _discountByAmount;
        private readonly IDiscountByPercentage _discountByPercentage;

        public DiscountTypeFactory(IDiscountByAmount discountByAmount, IDiscountByPercentage discountByPercentage)
        {
            this._discountByAmount = discountByAmount ?? throw new ArgumentNullException(nameof(discountByAmount));
            this._discountByPercentage = discountByPercentage ?? throw new ArgumentNullException(nameof(discountByPercentage));
        }

        public IDiscountType Create(DiscountTypeEnum discountTypeEnum)
        {
            switch (discountTypeEnum)
            {
                case DiscountTypeEnum.Amount:
                    return _discountByAmount as IDiscountType;
                case DiscountTypeEnum.Percentage:
                    return _discountByPercentage as IDiscountType;
                default:
                    throw new ArgumentOutOfRangeException(nameof(discountTypeEnum));
            }
        }
    }
}
