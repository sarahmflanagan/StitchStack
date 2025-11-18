namespace StitchStack.Tests.TestSetup;

public abstract class IntegrationTestBase: IAsyncLifetime
{
    protected readonly TestWebApplicationFactory _factory;
    protected  HttpClient _client { get; private set; }

    protected IntegrationTestBase()
    {
        _factory = new TestWebApplicationFactory();
    }

    public async Task InitializeAsync()
    {
        await _factory.InitializeAsync();
        _client = _factory.CreateClient();
    }
    
    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}