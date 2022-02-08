namespace Clay.AccessControl.Api.Data;
public static class Seed
{
    public static Lock[] Locks = {
            new Lock (1,"The Brick House First Door (Tunnel)", new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa")),
            new Lock (2,"The Brick House Second Door (Office)", new Guid ("d5bc20c6-4a33-4b18-aa89-589b1e3382df"))
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
            new LockTag (2,1),
            new LockTag (1,2),
            new LockTag (2,2),
            new LockTag (1,3),
            new LockTag (1,4),
            new LockTag (2,4),
            new LockTag (1,5),
            new LockTag (1,6),
            new LockTag (2,6)
        };

    public static Audit[] Audits =
    {
        new Audit {Id = 1,  LockId = 1, LockDescription = "The Brick House First Door (Tunnel)", LockToken = new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa"), TagId = 1, TagToken = new Guid ("6f5f6b36-ace9-401e-8e97-5dea550e2b3d"), UserId = 1, UserName = "Darjan", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 10, 10)},
        new Audit {Id = 2,  LockId = 2, LockDescription = "The Brick House Second Door (Office)", LockToken = new Guid ("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), TagId = 1, TagToken = new Guid ("6f5f6b36-ace9-401e-8e97-5dea550e2b3d"), UserId = 1, UserName = "Darjan", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 10, 20)},
        
        new Audit {Id = 3,  LockId = 1, LockDescription = "The Brick House First Door (Tunnel)", LockToken = new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa"), TagId = 2, TagToken = new Guid ("935007b3-0c77-4ed4-be51-e51d11c944ee"), UserId = 2, UserName = "Medi", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 15, 20)},
        new Audit {Id = 4,  LockId = 2, LockDescription = "The Brick House Second Door (Office)", LockToken = new Guid ("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), TagId = 2, TagToken = new Guid ("935007b3-0c77-4ed4-be51-e51d11c944ee"), UserId = 2, UserName = "Medi", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 15, 35)},
        
        new Audit {Id = 5,  LockId = 1, LockDescription = "The Brick House First Door (Tunnel)", LockToken = new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa"), TagId = 3, TagToken = new Guid ("a85b118a-95bf-4a31-8e07-d873c37434dd"), UserId = 3, UserName = "Daniel", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 30, 20)},
        new Audit {Id = 6,  LockId = 2, LockDescription = "The Brick House Second Door (Office)", LockToken = new Guid ("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), TagId = 3, TagToken = new Guid ("a85b118a-95bf-4a31-8e07-d873c37434dd"), UserId = 3, UserName = "Daniel", AccessResult = "NOT AUTHORIZED :: Tag not authorized", ActionedAt = new DateTime(2022, 01, 10, 8, 30, 30)},
        new Audit {Id = 7,  LockId = 2, LockDescription = "The Brick House Second Door (Office)", LockToken = new Guid ("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), TagId = 4, TagToken = new Guid ("5f43e5a1-13fc-4d28-9d60-35cde5c0bc8e"), UserId = 3, UserName = "Daniel", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 30, 33)},

        new Audit {Id = 8,  LockId = 1, LockDescription = "The Brick House First Door (Tunnel)", LockToken = new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa"), TagId = 5, TagToken = new Guid ("207340c0-b073-4d16-a8c5-819a0117c9cb"), UserId = 4, UserName = "Peter", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 45, 5)},
        new Audit {Id = 9,  LockId = 2, LockDescription = "The Brick House Second Door (Office)", LockToken = new Guid ("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), TagId = 5, TagToken = new Guid ("207340c0-b073-4d16-a8c5-819a0117c9cb"), UserId = 4, UserName = "Peter", AccessResult = "NOT AUTHORIZED :: Tag not authorized", ActionedAt = new DateTime(2022, 01, 10, 8, 45, 10)},
        
        new Audit {Id = 10, LockId = 1, LockDescription = "The Brick House First Door (Tunnel)", LockToken = new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa"), TagId = 6, TagToken = new Guid ("f330243e-3314-4f41-9bd3-8577b2faf823"), UserId = 5, UserName = "Amin", AccessResult = "NOT AUTHORIZED :: Tag is not active", ActionedAt = new DateTime(2022, 01, 10, 8, 50, 10)},
        new Audit {Id = 11, LockId = 1, LockDescription = "The Brick House First Door (Tunnel)", LockToken = new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa"), TagId = 6, TagToken = new Guid ("f330243e-3314-4f41-9bd3-8577b2faf823"), UserId = 5, UserName = "Amin", AccessResult = "NOT AUTHORIZED :: Tag is not active", ActionedAt = new DateTime(2022, 01, 10, 8, 50, 12)},
        new Audit {Id = 12, LockId = 1, LockDescription = "The Brick House First Door (Tunnel)", LockToken = new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa"), TagId = 6, TagToken = new Guid ("f330243e-3314-4f41-9bd3-8577b2faf823"), UserId = 5, UserName = "Amin", AccessResult = "NOT AUTHORIZED :: Tag is not active", ActionedAt = new DateTime(2022, 01, 10, 8, 50, 14)},

        new Audit {Id = 13, LockId = 1, LockDescription = "The Brick House First Door (Tunnel)", LockToken = new Guid ("7025cdba-4810-47d9-acdc-99f48766c0aa"), TagId = 4, TagToken = new Guid ("5f43e5a1-13fc-4d28-9d60-35cde5c0bc8e"), UserId = 3, UserName = "Daniel", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 55, 40)},
        new Audit {Id = 14, LockId = 2, LockDescription = "The Brick House Second Door (Office)", LockToken = new Guid ("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), TagId = 4, TagToken = new Guid ("5f43e5a1-13fc-4d28-9d60-35cde5c0bc8e"), UserId = 3, UserName = "Daniel", AccessResult = "Opened", ActionedAt = new DateTime(2022, 01, 10, 8, 55, 45)},
    };
}