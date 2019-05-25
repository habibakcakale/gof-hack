namespace Hack.Domain
{
    public class CompletedWorkItem : WorkItem
    {
        public int UserId { get; set; }
        /// <summary>
        /// Assigned User.
        /// </summary>
        public User User { get; set; }
        public int TimeSpent { get; set; }
    }
}