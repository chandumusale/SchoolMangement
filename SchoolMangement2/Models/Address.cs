using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMangement2.Models
{
    public class Address
    {
        [ForeignKey("EmployeeId")]
        public int Id { get; set; }
       
        public string? AddressLine { get; set; }

        [Required(ErrorMessage ="City Name is Required")]
        public string? City { get; set; }

        [StringLength(20,ErrorMessage ="Invalid State Name")]
        public string? State { get; set; }

        [Range(6,8,ErrorMessage ="Pincode Should be in range Between 6 to 8")]
        public long PinCode { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}
