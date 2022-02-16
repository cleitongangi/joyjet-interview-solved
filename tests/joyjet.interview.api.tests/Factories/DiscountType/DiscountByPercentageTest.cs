using joyjet_interview_test.Factories.DiscountType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace joyjet_interview_test.tests.Factories.DiscountType
{
    public class DiscountByPercentageTest
    {
        [Theory]
        [InlineData(100, 100, 0)]
        [InlineData(110, 100, 0)]
        [InlineData(30, 100, 70)]
        [InlineData(33, 100, 67)]
        [InlineData(33, 99, 66)]
        [InlineData(25, 100, 75)]
        [InlineData(25, 99, 74)]
        [InlineData(30, 1358, 951)]
        [InlineData(29, 1359, 965)]
        public void GetDiscountedPrice_ValidValues_ReturnRight(long discountValue, long price, long expected)
        {
            // Arrange
            var discountByPercentage = new DiscountByPercentage();

            // Act
            var result = discountByPercentage.GetDiscountedPrice(discountValue, price);

            // Assert
            Assert.Equal(result, result);
        }
    }
}
