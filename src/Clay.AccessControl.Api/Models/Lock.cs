namespace Clay.AccessControl.Api.Models;
public class Lock
{
    public Lock(int id, string description, Guid token)
    {
        Id = id;
        Description = description;
        Token = token;
    }

    public int Id { get; set; }
    public string Description { get; set; } = "";
    public Guid Token { get; set; }
    public ICollection<Tag> AuthorizedTags { get; set; } = default!;
    public List<LockTag> LockTags { get; set; } = default!;
}