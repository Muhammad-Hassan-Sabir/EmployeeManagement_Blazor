using EmployeeManagement_Blazor.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Dynamic.Core;

namespace EmployeeManagement_Blazor.Server.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (employee.Department!=null)
            {
                context.Entry(employee.Department).State = EntityState.Unchanged;
            }

            var res = await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            return res.Entity;




        }

        public async Task DeleteEmployee(int employeeId)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await context.Employees.ToListAsync();

        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            var employee = await context.Employees
                                        .Include(x => x.Department)
                                        .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            return employee;

        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var employee = await context.Employees
                                        .Include(x => x.Department)
                                        .FirstOrDefaultAsync(x => x.Email == email);

            return employee;
        }

        public async Task<EmployeeDataResult> GetEmployees(int skip,int take,string orderBy)
        {
            return new EmployeeDataResult
            {
                Employees = context.Employees.OrderBy(orderBy).Skip(skip).Take(take),
                Count = await context.Employees.CountAsync() 
            };
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query = context.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));

            }

            if (gender != null)
            {
                query.Where(x => x.Gender == gender);
            }


            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await context.Employees
               .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.DateOfBrith = employee.DateOfBrith;
                result.Gender = employee.Gender;
                if (employee.DepartmentId != 0)
                {
                    result.DepartmentId = employee.DepartmentId;
                }
                else if (employee.Department != null)
                {
                    result.DepartmentId = employee.Department.DepartmentId;
                }
                result.PhotoPath = employee.PhotoPath;

                await context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        
    }
}
