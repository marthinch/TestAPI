using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.Models;
using Test.Paging;

namespace Test.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>();

        public EmployeesController()
        {
            for (int i = 0; i < 10; i++)
            {
                employees.Add(new Employee { Id = i + 1, Name = "Employee " + i, Phone = i.ToString() });
            }
        }

        [HttpGet("/api/employees")]
        public IEnumerable<Employee> Employees([FromQuery] EmployeePaging paging)
        {
            if(!string.IsNullOrEmpty(paging.Search))
                employees = employees.Where(a => a.Name == paging.Search).ToList();

            if(!string.IsNullOrEmpty(paging.SortBy))
            {
                if(paging.SortBy == "Id")
                {
                    employees = employees.OrderBy(a => a.Id).ToList();
                }
            }

            return employees;
        }

        [HttpGet("/api/employees/{id}")]
        public Employee Employee(int id)
        {
            var employee = employees.Where(a => a.Id == id).FirstOrDefault();
            if (employee == null)
                throw new Exception("employee not found");

            return employee;
        }

        [HttpPost("/api/employees")]
        public bool CreateEmployee(Employee employee)
        {
            employees.Add(employee);
            return true;
        }

        [HttpPut("/api/employees/{id}")]
        public bool UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = employees.Where(a => a.Id == id).FirstOrDefault();
            if (existingEmployee == null)
                return false;

            employees.Remove(existingEmployee);

            employees.Add(employee);
            return true;
        }

        [HttpDelete("/api/employees/{id}")]
        public bool DeleteEmployee(int id)
        {
            var existingEmployee = employees.Where(a => a.Id == id).FirstOrDefault();
            if (existingEmployee == null)
                return false;

            employees.Remove(existingEmployee);
            return true;
        }
    }
}
