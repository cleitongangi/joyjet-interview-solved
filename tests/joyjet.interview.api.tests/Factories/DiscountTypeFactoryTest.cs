using joyjet_interview_test.Enums;
using joyjet_interview_test.Factories;
using joyjet_interview_test.Factories.DiscountType;
using joyjet_interview_test.Interfaces.Factories.DiscountType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace joyjet_interview_test.tests.Factories
{
    public class DiscountTypeFactoryTest
    {
        [Fact]
        public void Create_ReturnDiscountedByAmount_ReturnRight()
        {
            // Arrange
            var discountTypeFactory = new DiscountTypeFactory(new DiscountByAmount(), new DiscountByPercentage());

            // Act
            var result = discountTypeFactory.Create(DiscountTypeEnum.Amount);

            // Assert
            Assert.IsAssignableFrom<IDiscountByAmount>(result);
        }

        [Fact]
        public void Create_ReturnDiscountedByPercentage_ReturnRight()
        {
            // Arrange
            var discountTypeFactory = new DiscountTypeFactory(new DiscountByAmount(), new DiscountByPercentage());

            // Act
            var result = discountTypeFactory.Create(DiscountTypeEnum.Percentage);

            // Assert
            Assert.IsAssignableFrom<IDiscountByPercentage>(result);
        }
    }
}
