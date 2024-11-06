
using System.ComponentModel;

namespace examApi.Models
{
    public class Employee
    {
        [DisplayName("Employee Id")]
        public int EmpId { get; set; }
        [DisplayName("Employee Name")]
        public string EmpName { get; set; } = null!;
        [DisplayName("Department Id")]
        public int DeptId { get; set; }
        [DisplayName("Library Id")]
        public int LibId { get; set; }

        
    }
}
