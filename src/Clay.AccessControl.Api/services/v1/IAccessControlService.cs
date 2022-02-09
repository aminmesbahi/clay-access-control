namespace Clay.AccessControl.Api.Services;
public interface IAccessControlService
{
    public Task<bool> AccessRequestAsync(AccessRequestDto request, System.Net.IPAddress remoteIpAddress);
    public Task Audit(Audit audit);
    public Task<GetAuditListResponseDto> GetAccessHistoryByPageAsync(int limit, int page, CancellationToken cancellationToken);
}