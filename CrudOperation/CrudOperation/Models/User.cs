using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CrudOperation.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Full Name cannot be longer than 20 characters.")]
        public string? FullName { get; set; }

        public string? ProfileImageBase64 { get; set; } // Store the image as a base64 string

        [Required]
        public string? Gender { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Address cannot be longer than 25 characters.")]
        public string? Address { get; set; }

        
        public string? City { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pin must be a 6-digit number.")]
        public int Pin { get; set; }

      
        public string? State { get; set; }

   
        public string? Country { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact must be a 10-digit number.")]
        public string? Contact { get; set; }

        public string? EducationQualification { get; set; }

        public string? Designation { get; set; }
    }

}
