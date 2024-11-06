using System.ComponentModel;

namespace examApi.Models
{
    public class Department
    {
        [DisplayName("Deptarment Id")]
        public int DeptId { get; set; }
        [DisplayName("Department Name")]
        public string DeptName { get; set; }

        [DisplayName("Deptarment Location")]
        public string DeptLoc { get; set; }
    }
}
