namespace Clay.AccessControl.Api.Models;
public class Tag
{
    public Tag()
    {

    }
    public Tag(int id, bool isActive, int ownerId, Guid token)
    {
        Id = id;
        IsActive = isActive;
        OwnerId = ownerId;
        Token = token;
    }

    public int Id { get; set; }
    public bool IsActive { get; set; }
    public User Owner { get; set; } = default!;
    public int OwnerId { get; set; }
    public Guid Token { get; set; }
    public ICollection<Lock> OpeningLocks { get; set; } = default!;
    public List<LockTag> LockTags { get; set; } = default!;
}
