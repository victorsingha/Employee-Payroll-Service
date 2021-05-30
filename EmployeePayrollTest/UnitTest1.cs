using EmployeePayrollService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace EmployeePayrollTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddEmployeeTest()
        {
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employee = new EmployeeModel();
            employee.EmployeeName = "Mohan";
            employee.Department = "Tech1";
            employee.PhoneNumber = "6302907678";
            employee.Address = "02-Patna";
            employee.Gender = 'M';
            employee.BasicPay = 10000.00M;
            employee.Deductions = 1500.00;
            employee.StartDate = Convert.ToDateTime("2020-11-03");
            //Mock<EmployeeRepo> mockObj = new Mock<EmployeeRepo>();
            //mockObj.Setup(t=>t.AddEmployee(It.IsIn<EmployeeModel>)).return (true);
            var result = repo.AddEmployee(employee);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetSumOfAllEmployeeBasicPay()
        {
            EmployeeRepo repo = new EmployeeRepo();
            decimal result = repo.getSumOfBasicPay();
            decimal expected = 1810054;
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetNameOfEmployeeBetweenGivenDateTest()
        {
            EmployeeRepo repo = new EmployeeRepo();
            repo.getNameOfEmployeeBetweenGivenDate("2019-01-01","2020-01-01");
        }

        [TestMethod]
        public void ThreadVSNoThread()
        {
            EmployeeRepo repo = new EmployeeRepo();

            DateTime startDateTime = DateTime.Now;
            repo.GetAllEmployee();
            DateTime stopDateTime = DateTime.Now;
            Console.WriteLine("Duration without thread: " + (stopDateTime - startDateTime));

            DateTime startDateTimeThread = DateTime.Now;
            DateTime stopDateTimeThread = DateTime.Now;
            Task thread = new Task(() =>
            {
                startDateTimeThread = DateTime.Now;
                repo.GetAllEmployee();
                stopDateTimeThread = DateTime.Now;
                Console.WriteLine("Duration with thread: " + (stopDateTimeThread - startDateTimeThread));
            }); thread.Start();

        }
    }
}
