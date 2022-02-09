namespace Clay.AccessControl.Api.Services;
public interface IAccessControlService
{
    public Task<bool> AccessRequestAsync(AccessRequestDto request, System.Net.IPAddress remoteIpAddress);
    public Task Audit(Audit audit);
    public Task<GetAuditListResponseDto> GetAccessHistoryByPageAsync(int limit, int page, CancellationToken cancellationToken);
    public Task<GetAuditListResponseDto> GetUserAccessHistoryByPageAsync(int userId, int limit, int page, CancellationToken cancellationToken);
    public Task<GetAuditListResponseDto> GetLockAccessHistoryByPageAsync(Guid LockToken, int limit, int page, CancellationToken cancellationToken);
}