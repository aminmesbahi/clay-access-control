namespace Clay.AccessControl.Api.Models {
    public class LockTag {
        public LockTag(int lockId, int tagId)
        {
            LockId = lockId;
            TagId = tagId;
        }

        public DateTime CreateDate { get; set; }=DateTime.Now;
        public int LockId { get; set; }
        public Lock Lock { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}