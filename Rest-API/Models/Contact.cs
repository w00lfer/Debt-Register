using System.ComponentModel.DataAnnotations;

namespace Rest_API.Models
{
    /// <summary>
    /// Mode for users without account for tracking debts for a specific person
    /// </summary>
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

    }
}
