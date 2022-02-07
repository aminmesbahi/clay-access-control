namespace Clay.AccessControl.Api.Models {
    public class User {
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public ICollection<Tag> OwnedTags { get; set; } = default!;
    }
}