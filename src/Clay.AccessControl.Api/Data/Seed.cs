using Clay.AccessControl.Api.Models;

namespace Clay.AccessControl.Api.Data;
public static class Seed
{
    public static Lock[] Locks = {
            new Lock (1,"The Brick House First Door (Tunnel)"),
            new Lock (2,"The Brick House Second Door (Office)")
        };

    public static User[] Users = {
            new User (1,"Darjan"),
            new User (2,"Medi"),
            new User (3,"Daniel"),
            new User (4,"Peter"),
            new User (5,"Amin")
        };

    public static Tag[] Tags = {
            new Tag (1, true, 1, new Guid ("6f5f6b36-ace9-401e-8e97-5dea550e2b3d")),
            new Tag (2, true, 2, new Guid ("935007b3-0c77-4ed4-be51-e51d11c944ee")),
            new Tag (3, true, 3, new Guid ("a85b118a-95bf-4a31-8e07-d873c37434dd")),
            new Tag (4, true, 3, new Guid ("5f43e5a1-13fc-4d28-9d60-35cde5c0bc8e")),
            new Tag (5, true, 4, new Guid ("207340c0-b073-4d16-a8c5-819a0117c9cb")),
            new Tag (6, false,5, new Guid ("f330243e-3314-4f41-9bd3-8577b2faf823")),
        };

    public static LockTag[] LockTags = {
            new LockTag (1,1),
            new LockTag (1,2),
            new LockTag (1,3),
            new LockTag (1,4),
            new LockTag (1,5),
            new LockTag (1,6)
        };
}