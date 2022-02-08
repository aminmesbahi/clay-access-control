using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Xunit.Abstractions;
namespace Clay.AccessControl.Api.Test;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = "Data Source=Application_UnitTests.db;Cache=Shared";
        // Add services to the container.
        services.AddDbContext<AccessControlDbContext>(x => x.UseSqlite(connectionString));
        services.AddScoped<IAccessControlService, AccessControlService>();
        var sp=services.BuildServiceProvider();
        var db = sp.CreateScope().ServiceProvider.GetRequiredService<AccessControlDbContext>();
        db.Database.MigrateAsync();
    }
}


public class AccessControl_AuthorizationTests
{    
    private readonly IAccessControlService _service;
    private readonly ITestOutputHelper _output;

    public AccessControl_AuthorizationTests( ITestOutputHelper output, IAccessControlService service)
    {
        _output = output;
        _service = service;
      
    }

    [Fact]
    public async Task Request_OpenDoorWithAuthorizedTag_ReturnsTrue()
    {
        //Arrange        
        var req = new AccessRequestDto { Lock = new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), Tag = new Guid("6f5f6b36-ace9-401e-8e97-5dea550e2b3d") };

        //Act
        var res = await _service.AccessRequestAsync(req);        

        //Assert
        Assert.True(res);
    }

    [Fact]
    public async Task Request_OpenDoorWithUnAuthorizedTag_ReturnsFalse()
    {
        //Arrange        
        var req = new AccessRequestDto { Lock = new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), Tag = new Guid("a85b118a-95bf-4a31-8e07-d873c37434dd") };

        //Act
        var res = await _service.AccessRequestAsync(req);

        //Assert
        Assert.False(res);
    }
}