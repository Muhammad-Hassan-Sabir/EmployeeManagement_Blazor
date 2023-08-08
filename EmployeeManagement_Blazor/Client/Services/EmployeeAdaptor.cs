using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace EmployeeManagement_Blazor.Client.Services
{
    public class EmployeeAdaptor:DataAdaptor
    {
        private readonly IEmployeeService employeeService;

        public EmployeeAdaptor(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string additionalParam = null)
        {

            string sorting = null;
            if (dataManagerRequest.Sorted!=null)
            {
                var sortList = dataManagerRequest.Sorted;
                sortList.Reverse();
                sorting = string.Join(",", sortList.Select(x => $"{x.Name} {x.Direction}"));
            }


            var result= await employeeService.GetEmployees(dataManagerRequest.Skip, dataManagerRequest.Take, sorting);

            var dataResult = new DataResult
            {
                Result = result.Employees,
                Count = result.Count
            };

            return dataResult;

        }
    }
}
