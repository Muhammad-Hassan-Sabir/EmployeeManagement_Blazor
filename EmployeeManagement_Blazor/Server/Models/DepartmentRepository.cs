using EmployeeManagement_Blazor.Shared;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManagement_Blazor.Server.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext context;

        public DepartmentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            var res = await context.Departments
                .FirstOrDefaultAsync(context => context.DepartmentId == departmentId);

            return res;

        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await context.Departments.ToListAsync();
        }
    }
}
