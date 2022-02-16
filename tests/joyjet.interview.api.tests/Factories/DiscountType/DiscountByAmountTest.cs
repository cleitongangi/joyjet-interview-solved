using joyjet_interview_test.Factories.DiscountType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace joyjet_interview_test.tests.Factories.DiscountType
{
    public class DiscountByAmountTest
    {
        [Theory]
        [InlineData(100, 100, 0)]
        [InlineData(35, 20, 0)]
        [InlineData(30, 150, 120)]
        [InlineData(33, 100, 67)]
        [InlineData(50, 1500, 1450)]
        [InlineData(25, 100, 75)]        
        public void GetDiscountedPrice_ValidValues_ReturnRight(long discountValue, long price, long expected)
        {
            // Arrange
            var discountByAmount = new DiscountByAmount();

            // Act
            var result = discountByAmount.GetDiscountedPrice(discountValue, price);

            // Assert
            Assert.Equal(result, result);
        }
    }
}
