using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeWebApliccatin
{
    public class Test
    {
        // public int Id { get; set; }
        //public string Name { get; set; }

        public static string connStr = ConfigurationManager.ConnectionStrings["EmployeeWeb"].ConnectionString;
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

        public static Test Get(int id)
        {

            Test output = null;
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

                    output = new Test();
                    output.Id = (int)dtr["Id"];
                    output.FirstName = dtr["FirstName"].ToString();
                    output.LastName = dtr["LastName"].ToString();
                }
                dtr.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return output;

            //return new Test();
        }
    }

  
}