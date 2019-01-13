using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeWebApliccatin
{
    public class Employee
    {

        public static string connStr = ConfigurationManager.ConnectionStrings["ProductsWeb"].ConnectionString;

        #region Variables

        private int id = 1;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string firstName = "";
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName = "";
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string tittle = "";
        public string Tittle
        {
            get { return tittle; }
            set { tittle = value; }
        }

        private DateTime? hiringDate = null;
        public DateTime? HiringDate
        {
            get { return hiringDate; }
            set { hiringDate = value; }
        }

        private string phoneNumber = "";
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string imgUrl = "";
        public string ImageUrl
        {
            get { return imgUrl; }
            set { imgUrl = value; }
        }

        #endregion


        public static List<Employee> GetEmployeeList()
        {

            List<Employee> employeeList = new List<Employee>();
            //Employee employee = new Employee();
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetEmployeeList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader dr;
           

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.Id= Convert.ToInt32(dr["ID"]);
                    employee.FirstName= dr["FirstName"].ToString();
                    employee.LastName = dr["LastName"].ToString();
                    employee.Tittle= dr["Tittle"].ToString();
                    if (dr["HiringDate"] == DBNull.Value)
                        employee.HiringDate=null;
                    else
                        employee.HiringDate = Convert.ToDateTime(dr["HiringDate"]);
                    employee.PhoneNumber= dr["PhoneNumber"].ToString();
                    employee.Description = dr["Description"].ToString();
                    employee.ImageUrl = dr["ImageUrl"].ToString();
                    employeeList.Add(employee);
                }
              
                dr.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return employeeList;
        }

        public static Employee GetEmployee(int id) {

            Employee output = null;
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            SqlDataReader dtr;

            try
            {
                param = new SqlParameter("Id", id);
                cmd.Parameters.Add(param);
                con.Open();
                dtr = cmd.ExecuteReader();

                while (dtr.Read())
                {

                    output = new Employee();
                    output.Id = (int)dtr["Id"];
                    output.FirstName = dtr["FirstName"].ToString();
                    output.LastName = dtr["LastName"].ToString();
                    //DBNull.Value ? null : Convert.ToDateTime(reader[3])
                }
                dtr.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return output;

        }

        public static void DeleteEmployee(int id) {

            
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            

            try
            {
                param = new SqlParameter("Id", id);
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteReader();
                con.Close();
               
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public static void UpdateEmployee(Employee employee)
        {
            
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                
                
                con.Open();
                            
                    cmd.Parameters.AddWithValue("@ID", (int)employee.Id);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName.ToString());
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName.ToString());
                    cmd.Parameters.AddWithValue("@Tittle", employee.Tittle.ToString());
                    if(employee.HiringDate == null)
                    cmd.Parameters.AddWithValue("@HiringDate",DBNull.Value); 
                    else
                    cmd.Parameters.AddWithValue("@HiringDate", Convert.ToDateTime(employee.HiringDate));
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch (Exception)
            {
                throw;
            }
          

        }


    }
}