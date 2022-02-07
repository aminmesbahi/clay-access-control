using Clay.AccessControl.Api.Data;
using Clay.AccessControl.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Clay.AccessControl.Api.Services;
public class AccessControlService : IAccessControlService
{
    private readonly AccessControlDbContext _context;

    public AccessControlService(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetRequiredService<AccessControlDbContext>();
    }

    public bool AccessRequest(Guid Lock, Guid Tag)
    {
        throw new NotImplementedException();
    }

    public async Task<GetAuditListResponseDto> GetAccessHistoryByPageAsync(int limit, int page, CancellationToken cancellationToken)
    {
        var history = await _context.Audits
                           .AsNoTracking()
                           .OrderBy(p => p.ActionedAt)
                           .PaginateAsync(page, limit, cancellationToken);

        return new GetAuditListResponseDto
        {
            CurrentPage = history.CurrentPage,
            TotalPages = history.TotalPages,
            TotalItems = history.TotalItems,
            Items = history.Items.Select(i => new GetAuditResponseDto
            {
                Id = i.Id,
                UserId = i.UserId,
                UserName = i.UserName,
                TagId = i.TagId,
                TagToken = i.TagToken,
                LockId = i.LockId,
                LockDescription = i.LockDescription,
                LockToken = i.LockToken,
                AccessResult = i.AccessResult,
                ClientIp = i.ClientIp,
                ActionedAt = i.ActionedAt
            }).ToList()
        };
    }
}