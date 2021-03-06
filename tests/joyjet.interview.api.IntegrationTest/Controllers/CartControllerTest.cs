using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using FluentAssertions.Json;

namespace joyjet.interview.api.IntegrationTest
{
    public class CartControllerTest
    {
        [Fact]
        public async Task PostCart_ValidJson_ReturnRight()
        {
            // Arrange
            var apiFactory = new CustomWebApiFactory();
            var client = apiFactory.CreateClient();
            var jsonData = @"{" +
                "  \"articles\": [" +
                "    { \"id\": 1, \"name\": \"water\", \"price\": 100 }," +
                "    { \"id\": 2, \"name\": \"honey\", \"price\": 200 }," +
                "    { \"id\": 3, \"name\": \"mango\", \"price\": 400 }," +
                "    { \"id\": 4, \"name\": \"tea\", \"price\": 1000 }," +
                "    { \"id\": 5, \"name\": \"ketchup\", \"price\": 999 }," +
                "    { \"id\": 6, \"name\": \"mayonnaise\", \"price\": 999 }," +
                "    { \"id\": 7, \"name\": \"fries\", \"price\": 378 }," +
                "    { \"id\": 8, \"name\": \"ham\", \"price\": 147 }" +
                "  ]," +
                "  \"carts\": [" +
                "    {" +
                "      \"id\": 1," +
                "      \"items\": [" +
                "        { \"article_id\": 1, \"quantity\": 6 }," +
                "        { \"article_id\": 2, \"quantity\": 2 }," +
                "        { \"article_id\": 4, \"quantity\": 1 }" +
                "      ]" +
                "    }," +
                "    {" +
                "      \"id\": 2," +
                "      \"items\": [" +
                "        { \"article_id\": 2, \"quantity\": 1 }," +
                "        { \"article_id\": 3, \"quantity\": 3 }" +
                "      ]" +
                "    }," +
                "    {" +
                "      \"id\": 3," +
                "      \"items\": [" +
                "        { \"article_id\": 5, \"quantity\": 1 }," +
                "        { \"article_id\": 6, \"quantity\": 1 }" +
                "      ]" +
                "    }," +
                "    {" +
                "      \"id\": 4," +
                "      \"items\": [" +
                "        { \"article_id\": 7, \"quantity\": 1 }" +
                "      ]" +
                "    }," +
                "    {" +
                "      \"id\": 5," +
                "      \"items\": [" +
                "        { \"article_id\": 8, \"quantity\": 3 }" +
                "      ]" +
                "    }" +
                "  ]," +
                "  \"delivery_fees\": [" +
                "    {" +
                "      \"eligible_transaction_volume\": {" +
                "        \"min_price\": 0," +
                "        \"max_price\": 1000" +
                "      }," +
                "      \"price\": 800" +
                "    }," +
                "    {" +
                "      \"eligible_transaction_volume\": {" +
                "        \"min_price\": 1000," +
                "        \"max_price\": 2000" +
                "      }," +
                "      \"price\": 400" +
                "    }," +
                "    {" +
                "      \"eligible_transaction_volume\": {" +
                "        \"min_price\": 2000," +
                "        \"max_price\": null" +
                "      }," +
                "      \"price\": 0" +
                "    }" +
                "  ]," +
                "  \"discounts\": [" +
                "    { \"article_id\": 2, \"type\": \"amount\", \"value\": 25 }," +
                "    { \"article_id\": 5, \"type\": \"percentage\", \"value\": 30 }," +
                "    { \"article_id\": 6, \"type\": \"percentage\", \"value\": 30 }," +
                "    { \"article_id\": 7, \"type\": \"percentage\", \"value\": 25 }," +
                "    { \"article_id\": 8, \"type\": \"percentage\", \"value\": 10 }" +
                "  ]" +
                "}";

            // Act
            var response = await client.PostAsync("/api/cart", new StringContent(jsonData, Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PostCartResult>(content);
            var expected = JsonConvert.DeserializeObject<PostCartResult>(@"                
                    {
                      'carts': [
                        {
                          'id': 1,
                          'total': 2350
                        },
                        {
                          'id': 2,
                          'total': 1775
                        },
                        {
                          'id': 3,
                          'total': 1798
                        },
                        {
                          'id': 4,
                          'total': 1083
                        },
                        {
                          'id': 5,
                          'total': 1196
                        }
                      ]                    
                }");

            expected.Should().BeEquivalentTo(result);            
        }

    }
}

public class PostCartResult
{
    public IEnumerable<Cart> Carts { get; set; }

    public PostCartResult(IEnumerable<Cart> carts)
    {
        Carts = carts;
    }
}

public class Cart
{
    public int Id { get; set; }
    public long Total { get; set; }
        
    public Cart(int id, long total)
    {
        Id = id;
        Total = total;
    }
}