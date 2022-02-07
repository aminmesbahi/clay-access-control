namespace Clay.AccessControl.Api.Models {
    public class Lock {
        public Lock(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; set; }
        public string Description { get; set; } = "";
        public ICollection<Tag> AuthorizedTags { get; set; } = default!;
        public List<LockTag> LockTags { get; set; }
    }
}