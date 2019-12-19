using System.ComponentModel.DataAnnotations;

namespace Rest_API.Models
{
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage = "Creator id is required")]
        public int CreatorId { get; set; }

        [Required(ErrorMessage = "Fullname is required")]
        [RegularExpression(@"^([A-Za-z]{3,})+\s+([A-Za-z]{3,})+$", ErrorMessage = "Fullname must contain fullname and surrname, each one with atleast 3 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Fullname is required")]
        [RegularExpression(@"^([0-9]{3})([0-9]{3})([0-9]{3})$", ErrorMessage ="Phone number is invalid")]
        public string PhoneNumber { get; set; }

    }
}
