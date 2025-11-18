using System.Net.Http.Json;
using StitchStack.Models;
using StitchStack.Tests.TestSetup;

namespace StitchStack.Tests.ControllerIntegrationTests
{
    public class FabricControllerIntegrationTests
    : IntegrationTestBase
    {

        public FabricControllerIntegrationTests()
        {
            // _client created in base class
        }

        [Fact]
        public async Task GetsAll_EndpointReturnsSuccessAndAnItem()
        {
            // Act
            var response = await _client.GetAsync("/api/Fabric");
        
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Equals(typeof(List<Fabric>)); // seeded data from DataSeeder.cs
        }

        [Fact]
        public async Task Get_EndpointReturnsSuccessAndAnItem()
        {
            // Act
            var response = await _client.GetAsync("/api/Fabric/1");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Equals(new Fabric()
            {
                Id = 1,
                Type = "Cotton",
            }); // seeded data from DataSeeder.cs
        }

        [Fact]
        public async Task Get_EndpointReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("/api/Fabric/9999");
        
            // Assert
            response.StatusCode.Equals("404"); // Status Code Not Found
        }
        
        [Fact]
        public async Task Create_EndpointCreatesNewItemAndReturnsCreated()
        {
            // Arrange
            var newItem = new Fabric()
            {
                Id = 5555,
                Type = "create_test",
            };
            JsonContent content = JsonContent.Create(newItem);
        
            // Act
            var response = await _client.PostAsync("/api/Fabric", content);
        
            // Assert
            response.StatusCode.Equals("204"); // Created
        }
        
        [Fact]
        public async Task Create_EndpointFailsToCreateANewItemDueToBadRequest()
        {
            // Act
            var response = await _client.PostAsync("/api/Fabric ", null);
        
            // Assert
            response.StatusCode.Equals("400"); // BadRequest
        }
        
        [Fact]
        public async Task Update_EndpointUpdatesItemAndReturnsNoContent()
        {
            // Arrange
            var updatedItem = new Fabric()
            {
                Id = 1,
                Type = "test_updated",
            };
            JsonContent content = JsonContent.Create(updatedItem);
        
            // Act
            var response = await _client.PutAsync("/api/Fabric/1", content);
        
            // Assert
            response.StatusCode.Equals("201"); // No Content
        }
    }
}