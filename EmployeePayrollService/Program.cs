using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll!");
            Console.WriteLine("----------------------------");
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employee = new EmployeeModel();
            //employee.EmployeeName = "Mohan";
            //employee.Department = "IT";
            //employee.PhoneNumber = "6302907678";
            //employee.Address = "02-Patna";
            //employee.Gender = 'M';
            //employee.BasicPay = 10000.00M;
            //employee.Deductions = 1500.00;
            //employee.StartDate = Convert.ToDateTime("2020-11-03");

            //if (repo.AddEmployee(employee))
            //    Console.WriteLine("Records added successfully");
            //repo.GetAllEmployee();

            //repo.UpdateSalary(700054, "Mark");

            //repo.getNameOfEmployeeBetweenGivenDate("2019-01-01", "2021-12-01");

            //DateTime startDateTime = DateTime.Now;
            //repo.GetAllEmployee();
            //DateTime stopDateTime = DateTime.Now;
            //Console.WriteLine("Duration without thread: " + (stopDateTime - startDateTime));

            //DateTime startDateTimeThread = DateTime.Now;
            //DateTime stopDateTimeThread = DateTime.Now;
            //Task thread = new Task(()=>
            //{
            //    startDateTimeThread = DateTime.Now;
            //    repo.GetAllEmployee();
            //    stopDateTimeThread = DateTime.Now;
            //    Console.WriteLine("Duration with thread: " + (stopDateTimeThread - startDateTimeThread));
            //});thread.Start();

            employee.EmployeeName = "raja";
            employee.BasicPay = 345435;
            employee.StartDate = Convert.ToDateTime("2022-10-21");
            employee.Gender = 'M';
            repo.InsertData_Transaction(employee);




            Console.ReadKey();
        }
    }
}
