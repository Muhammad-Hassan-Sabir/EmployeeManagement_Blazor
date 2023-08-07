using EmployeeManagement_Blazor.Shared;

namespace EmployeeManagement_Blazor.Server.Models
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int departmentId);

    }
}
