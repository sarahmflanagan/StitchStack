using System.Net.Http.Json;
using StitchStack.Models;
using StitchStack.Tests.TestSetup;

namespace StitchStack.Tests.ControllerIntegrationTests
{
    public class PatternControllerIntegrationTests
    : IntegrationTestBase
    {
        public PatternControllerIntegrationTests()
        {
            // _client created in base class
        }

        [Fact]
        public async Task GetsAll_EndpointReturnsSuccessAndAnItem()
        {
            // Act
            var response = await _client.GetAsync("/api/Pattern");
        
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Equals(typeof(List<Pattern>)); // seeded data from DataSeeder.cs
        }

        [Fact]
        public async Task Get_EndpointReturnsSuccessAndAnItem()
        {
            // Act
            var response = await _client.GetAsync("/api/Pattern/1");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Equals(new Pattern()
            {
                Id = 1,
                Name = "Simple A-Line Dress",
            }); // seeded data from DataSeeder.cs
        }

        [Fact]
        public async Task Get_EndpointReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("/api/Pattern/9999");
        
            // Assert
            response.StatusCode.Equals("404"); // Status Code Not Found
        }
        
        [Fact]
        public async Task Create_EndpointCreatesNewItemAndReturnsCreated()
        {
            // Arrange
            var newItem = new Pattern()
            {
                Id = 5555,
                Name = "create_test",
            };
            JsonContent content = JsonContent.Create(newItem);
        
            // Act
            var response = await _client.PostAsync("/api/Pattern", content);
        
            // Assert
            response.StatusCode.Equals("204"); // Created
        }
        
        [Fact]
        public async Task Create_EndpointFailsToCreateANewItemDueToBadRequest()
        {
            // Act
            var response = await _client.PostAsync("/api/Pattern/6666 ", null);
        
            // Assert
            response.StatusCode.Equals("400"); // BadRequest
        }
        
        [Fact]
        public async Task Update_EndpointUpdatesItemAndReturnsNoContent()
        {
            // Arrange
            var updatedItem = new Pattern()
            {
                Id = 1,
                Name = "test_updated",
            };
            JsonContent content = JsonContent.Create(updatedItem);
        
            // Act
            var response = await _client.PutAsync("/api/Pattern/1", content);
        
            // Assert
            response.StatusCode.Equals("201"); // No Content
        }
    }
}