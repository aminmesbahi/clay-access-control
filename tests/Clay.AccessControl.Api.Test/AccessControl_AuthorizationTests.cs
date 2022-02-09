using Microsoft.Extensions.DependencyInjection;
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
        db.Database.EnsureDeleted();
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
        var res = await _service.AccessRequestAsync(req, System.Net.IPAddress.Parse("127.0.0.1"));        

        //Assert
        Assert.True(res);
    }

    [Fact]
    public async Task Request_OpenDoorWithUnAuthorizedTag_ReturnsFalse()
    {
        //Arrange        
        var req = new AccessRequestDto { Lock = new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), Tag = new Guid("a85b118a-95bf-4a31-8e07-d873c37434dd") };

        //Act
        var res = await _service.AccessRequestAsync(req, System.Net.IPAddress.Parse("127.0.0.1"));

        //Assert
        Assert.False(res);
    }

    [Fact]
    public async Task Request_OpenDoorWithInactiveTag_ReturnsFalse()
    {
        //Arrange        
        var req = new AccessRequestDto { Lock = new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), Tag = new Guid("f330243e-3314-4f41-9bd3-8577b2faf823") };

        //Act
        var res = await _service.AccessRequestAsync(req, System.Net.IPAddress.Parse("127.0.0.1"));

        //Assert
        Assert.False(res);
    }
    [Fact]
    public async Task Audit_OpenDoorWithAuthorizedTag_WritesOpened()
    {
        //Arrange        
        var req = new AccessRequestDto { Lock = new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), Tag = new Guid("6f5f6b36-ace9-401e-8e97-5dea550e2b3d") };
        var auditsBefore = _service.GetAccessHistoryByPageAsync(100,1, new CancellationToken()).Result.TotalItems;
        //Act
        var res = await _service.AccessRequestAsync(req, System.Net.IPAddress.Parse("127.0.0.1"));
        var auditsAfter = _service.GetAccessHistoryByPageAsync(100, 1, new CancellationToken()).Result.TotalItems;
        var auditRecord = _service.GetAccessHistoryByPageAsync(1, 1, new CancellationToken()).Result.Items.First();
        //Assert
        Assert.True(res && (auditsAfter - auditsBefore)==1 && auditRecord.AccessResult=="Opened");
    }

    [Fact]
    public async Task Audit_OpenDoorWithUnAuthorizedTag_WritesAunauthorized()
    {
        //Arrange        
        var req = new AccessRequestDto { Lock = new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), Tag = new Guid("a85b118a-95bf-4a31-8e07-d873c37434dd") };
        var auditsBefore = _service.GetAccessHistoryByPageAsync(100, 1, new CancellationToken()).Result.TotalItems;
        //Act
        var res = await _service.AccessRequestAsync(req, System.Net.IPAddress.Parse("127.0.0.1"));
        var auditsAfter = _service.GetAccessHistoryByPageAsync(100, 1, new CancellationToken()).Result.TotalItems;
        var auditRecord = _service.GetAccessHistoryByPageAsync(1, 1, new CancellationToken()).Result.Items.First();
        //Assert
        Assert.True(!res && (auditsAfter - auditsBefore) == 1 && auditRecord.AccessResult == "NOT AUTHORIZED :: Tag not authorized");
    }

    [Fact]
    public async Task Audit_OpenDoorWithInactiveTag_WritesAunauthorizedInactive()
    {
        //Arrange        
        var req = new AccessRequestDto { Lock = new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), Tag = new Guid("f330243e-3314-4f41-9bd3-8577b2faf823") };
        var auditsBefore = _service.GetAccessHistoryByPageAsync(100, 1, new CancellationToken()).Result.TotalItems;
        //Act
        var res = await _service.AccessRequestAsync(req, System.Net.IPAddress.Parse("127.0.0.1"));
        var auditsAfter = _service.GetAccessHistoryByPageAsync(100, 1, new CancellationToken()).Result.TotalItems;
        var auditRecord = _service.GetAccessHistoryByPageAsync(1, 1, new CancellationToken()).Result.Items.First();
        //Assert
        Assert.True(!res && (auditsAfter - auditsBefore) == 1 && auditRecord.AccessResult == "NOT AUTHORIZED :: Tag is not active");
    }
}