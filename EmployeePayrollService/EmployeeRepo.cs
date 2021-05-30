using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    public class EmployeeRepo
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //public static string connectionString = "Data Source=.;Initial Catalog=payroll_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllEmployee()
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (con)
                {
                    string query = @"Select * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);

                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.BasicPay = dr.GetDecimal(2);
                            employeeModel.StartDate = dr.GetDateTime(3);
                            employeeModel.Gender = Convert.ToChar(dr.GetString(4));
                            //employeeModel.PhoneNumber = dr.GetString(5);
                            //employeeModel.Address = dr.GetString(6);
                            employeeModel.Department = dr.GetString(7);
                            //employeeModel.Deductions = dr.GetDouble(8);
                            //employeeModel.TaxablePay = dr.GetDouble(9);
                            //employeeModel.Tax = dr.GetDouble(10);
                            //employeeModel.NetPay = dr.GetDouble(11);
                            Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        //public void GetAllEmployee_With_Thread()
        //{
        //    SqlConnection con = new SqlConnection(connectionString);
        //    try
        //    {
        //        EmployeeModel employeeModel = new EmployeeModel();
        //        using (con)
        //        {
        //            string query = @"Select * from employee_payroll;";
        //            SqlCommand cmd = new SqlCommand(query, con);
        //            con.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
                        
        //                while (dr.Read())
        //                {
        //                    employeeModel.EmployeeID = dr.GetInt32(0);

        //                    employeeModel.EmployeeName = dr.GetString(1);
        //                    employeeModel.BasicPay = dr.GetDecimal(2);
        //                    employeeModel.StartDate = dr.GetDateTime(3);
        //                    employeeModel.Gender = Convert.ToChar(dr.GetString(4));
        //                    //employeeModel.PhoneNumber = dr.GetString(5);
        //                    //employeeModel.Address = dr.GetString(6);
        //                    employeeModel.Department = dr.GetString(7);
        //                    //employeeModel.Deductions = dr.GetDouble(8);
        //                    //employeeModel.TaxablePay = dr.GetDouble(9);
        //                    //employeeModel.Tax = dr.GetDouble(10);
        //                    //employeeModel.NetPay = dr.GetDouble(11);
        //                    Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
        //                    Console.WriteLine("\n");
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("No data found");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        System.Console.WriteLine(e.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    //var qury=values()
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    //command.Parameters.AddWithValue("@City", model.City);
                    //command.Parameters.AddWithValue("@Country", model.Country);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }
        public void UpdateSalary(double basic_pay, string name)
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = $"update employee_payroll set basic_pay = {basic_pay} where name = '{name}';";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        Console.WriteLine("Salary Updated");
                    }
                    else
                    {
                        Console.WriteLine("Not Updated");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public decimal getSumOfBasicPay()
        {
            decimal sum = 0;
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string querySUM = $"SELECT SUM(basic_pay) FROM employee_payroll;";
                    string queryAVG = $"SELECT AVG(basic_pay) FROM employee_payroll;";
                    string queryCOUNT = $"SELECT COUNT(basic_pay) FROM employee_payroll;";

                    SqlCommand cmd = new SqlCommand(querySUM, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            sum = dr.GetDecimal(0);
                        }
                        return sum;
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                        return sum;
                    }  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return sum;
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void getNameOfEmployeeBetweenGivenDate(string startDate,string endDate)
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = $"select name from employee_payroll where start between cast('{startDate}' as date) and cast('{endDate}' as date);";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string name = dr.GetString(0);
                            Console.WriteLine(name);
                        }                      
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        //public void InsertEmployeeRecord(EmployeeModel employee)
        //{
        //    employee.deduction = Convert.ToInt32(0.2 * employee.basicPay);
        //    employee.taxablePay = employee.basicPay - employee.deduction;
        //    employee.incomeTax = Convert.ToInt32(0.1 * employee.taxablePay);
        //    employee.netPay = employee.basicPay - employee.incomeTax;
        //    SqlConnection connection = new SqlConnection(connectionString);


        //    string storedProcedure = "sp_InsertEmployeePayrollDetails";
        //    string storedProcedurePayroll = "sp_InsertPayrollDetails";
        //    using (connection)
        //    {
        //        connection.Open();
        //        SqlTransaction transaction;
        //        transaction = connection.BeginTransaction("Insert Employee Transaction");
        //        try
        //        {
        //            SqlCommand sqlCommand = new SqlCommand(storedProcedure, connection, transaction);
        //            sqlCommand.CommandType = CommandType.StoredProcedure;
        //            sqlCommand.Parameters.AddWithValue("@StartDate", employee.startDate);
        //            sqlCommand.Parameters.AddWithValue("@Name", employee.name);
        //            sqlCommand.Parameters.AddWithValue("@Gender", employee.gender);
        //            sqlCommand.Parameters.AddWithValue("@PhoneNumber", employee.phoneNumber);
        //            sqlCommand.Parameters.AddWithValue("@Address", employee.address);
        //            SqlParameter outPutVal = new SqlParameter("@scopeIdentifier", SqlDbType.Int);
        //            outPutVal.Direction = ParameterDirection.Output;
        //            sqlCommand.Parameters.Add(outPutVal);

        //            sqlCommand.ExecuteNonQuery();
        //            SqlCommand sqlCommand1 = new SqlCommand(storedProcedurePayroll, connection, transaction);
        //            sqlCommand1.CommandType = CommandType.StoredProcedure;
        //            sqlCommand1.Parameters.AddWithValue("@ID", outPutVal.Value);
        //            sqlCommand1.Parameters.AddWithValue("@BasicPay", employee.basicPay);
        //            sqlCommand1.Parameters.AddWithValue("@Deduction", employee.deduction);
        //            sqlCommand1.Parameters.AddWithValue("@TaxablePay", employee.taxablePay);
        //            sqlCommand1.Parameters.AddWithValue("@IncomeTax", employee.incomeTax);
        //            sqlCommand1.Parameters.AddWithValue("@NetPay", employee.netPay);
        //            sqlCommand1.ExecuteNonQuery();
        //            transaction.Commit();
        //            connection.Close();
        //        }

        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            try
        //            {
        //                transaction.Rollback();
        //            }
        //            catch (Exception ex2)
        //            {

        //                Console.WriteLine(ex2.Message);
        //            }
        //        }
        //    }

        //}


    }
}
