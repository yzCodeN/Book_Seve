using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotText.Models
{
    public class Employee
    {
        //创建基本属性类，再创建另一个类EmployeeRepository来对齐数据进行保存和调用
        //员工的基本属性
        public string ID { get; private set; }
        public string Name { get;private set; }
        public string Gender { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Department { get; private set; }
        public Employee(string _ID,string _Name,string _Gender,DateTime _BirthDate,string _Department)
        {
            ID = _ID;
            Name = _Name;
            Gender = _Gender;
            BirthDate = _BirthDate;
            Department = _Department;
        }
    }
}