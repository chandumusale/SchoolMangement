using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolMangement2.Models
{
    public class Employee
    {
        
        public int EmployeeId { get; set; }

        [DisplayName("First Name")]
        [MaxLength(26)]
        public string? FirstName { get; set; }

        [MaxLength(26,ErrorMessage ="Data should be entered in given range")]
        public string? LastName { get; set; }
        



        public string? Gender{ get; set; }
       
        public int  Salary { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public List<Address> AddressList { get; set; }
        public Employee()
        {
            AddressList = new List<Address>();
        }


    }
}
