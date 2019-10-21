namespace Rest_API.Models
{
    /// <summary>
    /// Mode for users without account for tracking debts for a specific person
    /// </summary>
    public class LocalUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserPicture { get; set; }

    }
}
