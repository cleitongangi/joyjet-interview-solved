using joyjet_interview_test.Enums;
using joyjet_interview_test.Interfaces.Factories.DiscountType;

namespace joyjet_interview_test.Interfaces.Factories
{
    public interface IDiscountTypeFactory
    {
        IDiscountType Create(DiscountTypeEnum discountTypeEnum);
    }
}
