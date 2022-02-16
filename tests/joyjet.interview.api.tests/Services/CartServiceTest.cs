using FluentAssertions;
using joyjet_interview_test.ApiModels;
using joyjet_interview_test.Enums;
using joyjet_interview_test.Factories;
using joyjet_interview_test.Factories.DiscountType;
using joyjet_interview_test.Interfaces.Factories;
using joyjet_interview_test.Interfaces.Factories.DiscountType;
using joyjet_interview_test.Models;
using joyjet_interview_test.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace joyjet_interview_test.tests.Services
{
    public class CartServiceTest
    {
        [Fact]
        public void CalculateCart_ValidInput_ReturnRight()
        {
            // Arrange
            var articles = new List<ArticleModel>
            {
                new ArticleModel(1,"water",100),
                new ArticleModel(2,"honey",200),
                new ArticleModel(3,"mango",400),
                new ArticleModel(4,"tea",1000),
                new ArticleModel(5,"ketchup",999),
                new ArticleModel(6,"mayonnaise",999),
                new ArticleModel(7,"fries",378),
                new ArticleModel(8,"ham",147),
            };
            var carts = new List<CartModel>
            {
                new CartModel(id: 1,
                    items: new List<CartItemModel>
                    {
                        new CartItemModel(1,6),
                        new CartItemModel(2,2),
                        new CartItemModel(4,1)
                    }),
                new CartModel(id: 2,
                    items: new List<CartItemModel>
                    {
                        new CartItemModel(2,1),
                        new CartItemModel(3,3)
                    }),
                new CartModel(id: 3,
                    items: new List<CartItemModel>
                    {
                        new CartItemModel(5,1),
                        new CartItemModel(6,1)
                    }),
                new CartModel(id: 4,
                    items: new List<CartItemModel>
                    {
                        new CartItemModel(7,1)
                    }),
                new CartModel(id: 5,
                    items: new List<CartItemModel>
                    {
                        new CartItemModel(8,3)
                    })
            };
            var deliveryFees = new List<DeliveryFeeModel>
            {
                new DeliveryFeeModel(new EligibleTransactionVolumeModel(0,1000),800),
                new DeliveryFeeModel(new EligibleTransactionVolumeModel(1000,2000),400),
                new DeliveryFeeModel(new EligibleTransactionVolumeModel(2000,null),0)
            };
            var discounts = new List<DiscountModel>
            {
                new DiscountModel(2,DiscountTypeEnum.Amount,25),
                new DiscountModel(5,DiscountTypeEnum.Percentage,30),
                new DiscountModel(6,DiscountTypeEnum.Percentage,30),
                new DiscountModel(7,DiscountTypeEnum.Percentage,25),
                new DiscountModel(8,DiscountTypeEnum.Percentage,10)
            };

            var postInput = new PostCartInput(articles, carts, deliveryFees, discounts);
            var discountTypeFactory = new DiscountTypeFactory(new DiscountByAmount(), new DiscountByPercentage());
            var cartService = new CartService(discountTypeFactory);

            // Act
            var result = cartService.CalculateCart(postInput);

            // Assert
            var expected = new TestPostCartResult(new List<TestCart>
            {
                new TestCart(1,2350),
                new TestCart(2,1775),
                new TestCart(3,1798),
                new TestCart(4,1083),
                new TestCart(5,1196),
            });

            Assert.Equal(expected.Carts.Count(), result.Count());
            Assert.Equal(expected.Carts.ElementAt(0).Id, result.ElementAt(0).Id);
            Assert.Equal(expected.Carts.ElementAt(0).Total, result.ElementAt(0).Total);

            Assert.Equal(expected.Carts.ElementAt(1).Id, result.ElementAt(1).Id);
            Assert.Equal(expected.Carts.ElementAt(1).Total, result.ElementAt(1).Total);

            Assert.Equal(expected.Carts.ElementAt(2).Id, result.ElementAt(2).Id);
            Assert.Equal(expected.Carts.ElementAt(2).Total, result.ElementAt(2).Total);

            Assert.Equal(expected.Carts.ElementAt(3).Id, result.ElementAt(3).Id);
            Assert.Equal(expected.Carts.ElementAt(3).Total, result.ElementAt(3).Total);

            Assert.Equal(expected.Carts.ElementAt(4).Id, result.ElementAt(4).Id);
            Assert.Equal(expected.Carts.ElementAt(4).Total, result.ElementAt(4).Total);
        }

        [Fact]
        public void CalculateDiscountedPrice_NullDiscount_SubTotal()
        {
            // Arrange
            var mock = new Mock<IDiscountTypeFactory>();
            mock.Setup(m => m.Create(DiscountTypeEnum.Amount)).Returns(new DiscountByAmount());
            mock.Setup(m => m.Create(DiscountTypeEnum.Percentage)).Returns(new DiscountByPercentage());


            Type type = typeof(CartService);
            var cartService = Activator.CreateInstance(type, mock.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "CalculateDiscountedPrice" && x.IsPrivate)
                .First();

            if (method == null)
                throw new InvalidOperationException();

            //Act
            var calculateDiscountedPrice = (long)method.Invoke(cartService, new object[] { null, 10 });

            //Assert
            Assert.Equal(10, calculateDiscountedPrice);
            mock.Verify(mock => mock.Create(It.IsAny<DiscountTypeEnum>()), Times.Never());
        }

        [Fact]
        public void CalculateDiscountedPrice_NonNullDiscount_SubTotal()
        {
            // Arrange
            var mockDiscountByPercentage = new Mock<IDiscountType>();
            mockDiscountByPercentage.Setup(m => m.GetDiscountedPrice(15, 100)).Returns(15);

            var mockDiscountTypeFactory = new Mock<IDiscountTypeFactory>();
            mockDiscountTypeFactory.Setup(m => m.Create(It.IsAny<DiscountTypeEnum>())).Returns(mockDiscountByPercentage.Object);


            Type type = typeof(CartService);
            var cartService = Activator.CreateInstance(type, mockDiscountTypeFactory.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "CalculateDiscountedPrice" && x.IsPrivate)
                .First();

            if (method == null)
                throw new InvalidOperationException();

            var discount = new DiscountModel(1, DiscountTypeEnum.Percentage, 15);

            //Act
            var calculateDiscountedPrice = (long)method.Invoke(cartService, new object[] { discount, 100 });

            //Assert            
            mockDiscountTypeFactory.Verify(mock => mock.Create(It.IsAny<DiscountTypeEnum>()), Times.Once());
            mockDiscountByPercentage.Verify(mock => mock.GetDiscountedPrice(15, 100), Times.Once());
        }

        [Theory]
        [InlineData(0,15)]
        [InlineData(149, 15)]
        [InlineData(150, 10)]
        [InlineData(151, 10)]
        [InlineData(300, 2)]
        [InlineData(400, 2)]
        public void GetDeliveryFeeBySubTotalRange_NonNullFees_ReturnRightFee(long input, long expected)
        {
            // Arrange
            var mockDiscountTypeFactory = new Mock<IDiscountTypeFactory>();

            Type type = typeof(CartService);
            var cartService = Activator.CreateInstance(type, mockDiscountTypeFactory.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetDeliveryFeeBySubTotalRange" && x.IsPrivate)
                .First();

            if (method == null)
                throw new InvalidOperationException();

            var fees = new List<DeliveryFeeModel>
            {
                new DeliveryFeeModel(new EligibleTransactionVolumeModel(0,150),15),
                new DeliveryFeeModel(new EligibleTransactionVolumeModel(150,250),10),
                new DeliveryFeeModel(new EligibleTransactionVolumeModel(250,null),2)
            };

            //Act
            var result = (long)method.Invoke(cartService, new object[] { fees, input });

            //Assert
            Assert.Equal(expected, result);            
        }        
    }
}

public class TestPostCartResult
{
    public IEnumerable<TestCart> Carts { get; set; }

    public TestPostCartResult(IEnumerable<TestCart> carts)
    {
        Carts = carts;
    }
}

public class TestCart
{
    public int Id { get; set; }
    public long Total { get; set; }

    public TestCart(int id, long total)
    {
        Id = id;
        Total = total;
    }
}