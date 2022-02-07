namespace Clay.AccessControl.Api.Models;
public record GetAuditListResponseDto
{
    public int CurrentPage { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages { get; init; }
    public List<GetAuditResponseDto> Items { get; init; } = default!;
}

public record GetAuditResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = "";
    public int TagId { get; set; }
    public Guid TagToken { get; set; }
    public int LockId { get; set; }
    public string LockDescription { get; set; } = "";
    public Guid LockToken { get; set; }
    public string AccessResult { get; set; } = "";
    public string ClientIp { get; set; } = "";
    public DateTime ActionedAt { get; set; }
}