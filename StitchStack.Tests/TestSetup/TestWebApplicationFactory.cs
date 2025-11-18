using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StitchStack.Data;
using StitchStack.Data.InMemory;

namespace StitchStack.Tests.TestSetup;

public class TestWebApplicationFactory: WebApplicationFactory<Program>
{
    private static bool _initialized = false;
    private static readonly object _initialisedLock = new object();
    
    public async Task InitializeAsync()
    {
        lock (_initialisedLock)
        {
            if (_initialized)
                return;
            _initialized = true;
        }
        
        // Ensure the in-memory database is created and seeded only once
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<InMemoryDBContext>();
        await dbContext.Database.EnsureCreatedAsync();
        await DbSeeder.SeedAsync(dbContext);
    }
    
}