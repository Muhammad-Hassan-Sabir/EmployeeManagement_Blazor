using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement_Blazor.Shared
{
    public class EmployeeDataResult
    {
        public IEnumerable<Employee> Employees { get; set; }

        public int Count { get; set; }

    }
}
