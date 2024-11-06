using System.ComponentModel;

namespace examApi.Models
{
    public class EmpDept
    {
       [ DisplayName("Employee Name")]
        public string EmpName { get; set; }
        [DisplayName ("Depatment Name")]
        public string DeptName { get; set; }
         
    }
}

//[{"empName":"nbvcb","deptName":"CSE"},{ "empName":"B","deptName":"jhmghfgdzxfghj"},{ "empName":"dbndn ","deptName":"CSE"},{ "empName":"hfbfvdb","deptName":"jhmghfgdzxfghj"}]

//[{"employeeName":"nbvcb","departmentName":"CSE"},{ "employeeName":"B","departmentName":"jhmghfgdzxfghj"},{ "employeeName":"dbndn ","departmentName":"CSE"},{ "employeeName":"hfbfvdb","departmentName":"jhmghfgdzxfghj"}]