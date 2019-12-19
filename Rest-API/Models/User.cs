using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rest_API.Models
{
    public class User : IdentityUser<int>
    {
        [Required(ErrorMessage = "User fullname is required")]
        [Column(TypeName = "nvarchar(150)")]
        [RegularExpression(@"^([A-Za-z]{3,})+\s+([A-Za-z]{3,})+$")]
        public string FullName { get; set; }
    }
}
