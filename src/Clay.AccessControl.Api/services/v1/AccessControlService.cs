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

    public async Task<bool> AccessRequestAsync(AccessRequestDto request,System.Net.IPAddress remoteIpAddress)
    {
        if (!_context.Locks.Any(x => x.Token == request.Lock))
        {
            await MakeAuditAsync(request, false, remoteIpAddress);
            return false;
        }
            

        var lck = await _context.Tags.Include(x => x.OpeningLocks).Where(x => x.Token == request.Tag && x.IsActive == true && x.OpeningLocks.Count(y => y.Token == request.Lock) == 1).SingleOrDefaultAsync();
        await MakeAuditAsync(request, lck != null, remoteIpAddress);        
        return lck!=null;
    }

    public async Task Audit(Audit audit)
    {
        await _context.Audits.AddAsync(audit);
        await _context.SaveChangesAsync();

    }
    private async Task MakeAuditAsync(AccessRequestDto request, bool isSuccessful, System.Net.IPAddress remoteIpAddress)
    {
        var audit = new Audit();
        audit.ActionedAt = DateTime.UtcNow;
        audit.ClientIp = remoteIpAddress.ToString();
        audit.LockToken = request.Lock;
        audit.TagToken = request.Tag;
        audit.AccessResult = isSuccessful ? "Opened" : "NOT AUTHORIZED :: ";
        // When everything is OK
        if (isSuccessful)
        {
            var qry = await _context.Tags.Include(x => x.OpeningLocks).Include(x=>x.Owner).Include(x=>x.OpeningLocks).Where(x => x.Token == request.Tag).SingleOrDefaultAsync();
            audit.TagId = qry!.Id;
            audit.LockId=qry.OpeningLocks.Where(x=>x.Token==request.Lock).FirstOrDefault()!.Id;
            audit.LockDescription= qry.OpeningLocks.Where(x => x.Token == request.Lock).FirstOrDefault()!.Description;
            audit.UserName = qry.Owner.Name;
            audit.UserId = qry.OwnerId; 
        }
        else if(!_context.Locks.Any(x => x.Token == request.Lock))
        {
            audit.LockId = -1;
            audit.LockDescription = "NA";
            var qry = await _context.Tags.Include(x => x.Owner).Where(x => x.Token == request.Tag).SingleOrDefaultAsync();
            // When tag in not active OR tag exists but not authorized and Lock does not exists
            if (qry != null)
            {
                audit.TagId = qry.Id;
                audit.UserName = qry.Owner.Name;
                audit.UserId = qry.OwnerId;
                if (!qry.IsActive) { audit.AccessResult += "Tag is not active + Lock dos not exists"; }
                else if (qry.OpeningLocks.Where(x => x.Token == request.Tag).Count() == 0 && qry.IsActive) { audit.AccessResult += "Tag not authorized"; }
            }
            //When tag not exists and Lock does not exists
            else
            {
                audit.TagId = -1;
                audit.UserName = "NA";
                audit.UserId = -1;
                audit.AccessResult += "Tag not exists + Lock dos not exists";
            }
        }
        else
        {
            var qry = await _context.Tags.Include(x => x.OpeningLocks).Include(x => x.Owner).Include(x => x.OpeningLocks).Where(x => x.Token == request.Tag).SingleOrDefaultAsync();
            audit.LockId = _context.Locks.Where(x => x.Token == request.Lock).FirstOrDefault()!.Id;
            audit.LockDescription = _context.Locks.Where(x => x.Token == request.Lock).FirstOrDefault()!.Description;

            // When tag in not active OR tag exists but not authorized
            if (qry != null)
            {
                audit.TagId = qry.Id;
                audit.UserName = qry.Owner.Name;
                audit.UserId = qry.OwnerId;
                if (!qry.IsActive) { audit.AccessResult += "Tag is not active"; }
                else if (qry.OpeningLocks.Where(x => x.Token == request.Tag).Count() == 0 && qry.IsActive) { audit.AccessResult += "Tag not authorized"; }
            }
            //When tag not exists
            else
            {
                audit.TagId = -1;
                audit.UserName = "NA";
                audit.UserId = -1;
                audit.AccessResult += "Tag not exists";
            }
        }
 
        _context.Audits.Add(audit);
        await _context.SaveChangesAsync();
    }

    public async Task<GetAuditListResponseDto> GetAccessHistoryByPageAsync(int limit, int page, CancellationToken cancellationToken)
    {
        var history = await _context.Audits
                           .AsNoTracking()
                           .OrderByDescending(p => p.ActionedAt)
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

    public async Task<GetAuditListResponseDto> GetUserAccessHistoryByPageAsync(int userId, int limit, int page, CancellationToken cancellationToken)
    {
        var history = await _context.Audits
                           .Where(p => p.UserId == userId)
                           .AsNoTracking()
                           .OrderByDescending(p => p.ActionedAt)
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

    public async Task<GetAuditListResponseDto> GetLockAccessHistoryByPageAsync(Guid LockToken, int limit, int page, CancellationToken cancellationToken)
    {
        var history = await _context.Audits
                           .Where(x => x.LockToken == LockToken)
                           .AsNoTracking()
                           .OrderByDescending(p => p.ActionedAt)
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