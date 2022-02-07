namespace Clay.AccessControl.Api.Services;
public interface IAccessControlService
{
    public bool AccessRequest(Guid Lock, Guid Tag);
    public Task<GetAuditListResponseDto> GetAccessHistoryByPageAsync(int limit, int page, CancellationToken cancellationToken);
}