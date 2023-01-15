using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;

namespace Mathee_Steophanus_PRG282_ST
{
    internal class DataHandler
    {
        FileHandler fh = new FileHandler();
        SqlConnection conn = new SqlConnection(@"Data Source = localhost; Initial Catalog = StudentVaxinations; Integrated Security = True");
        public BindingSource bs = new BindingSource();
        SqlCommand cmd;

        public void GetData()
        {
            SqlDataReader reader;
            string query = @"SELECT * FROM Student";

            try
            {
                cmd = new SqlCommand(query, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                if(reader != null)
                {
                    bs.DataSource = reader;
                }
                else
                {
                    MessageBox.Show("There are no data in the current datatable");
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        public void GetStudent(int StudentNumber, string FirstName, string LastName, DateTime BirthDate)
        {
            SqlDataReader reader;
            try
            {
                cmd = new SqlCommand("GetStudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentNumber", StudentNumber);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    bs.DataSource = reader;
                }
                else
                {
                    MessageBox.Show("The student with that Student Number, First Name, Last Name or age is not in the Data Table");
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        public void AddStudent(int StudentNumber, string FirstName, string LastName, string Gender, DateTime BirthDate, string VaxinationStatus, string ExCovidStatus)
        {
            fh.Write($"{StudentNumber}, Added, {DateTime.Now}");
            try
            {
                cmd = new SqlCommand("AddStudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentNumber", StudentNumber);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                cmd.Parameters.AddWithValue("@VaxinationStatus", VaxinationStatus);
                cmd.Parameters.AddWithValue("@ExCovidStatus", ExCovidStatus);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("The student has successfully been added");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        public void UpdateStudent(int StudentNumber, string FirstName, string LastName, string Gender, DateTime BirthDate, string VaxinationStatus, string ExCovidStatus)
        {
            fh.Write($"{StudentNumber},{FirstName},{LastName},{Gender},{BirthDate},{VaxinationStatus},{ExCovidStatus}, Updated, {DateTime.Now}");
            try
            {
                cmd = new SqlCommand("UpdateStudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentNumber", StudentNumber);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                cmd.Parameters.AddWithValue("@VaxinationStatus", VaxinationStatus);
                cmd.Parameters.AddWithValue("@ExCovidStatus", ExCovidStatus);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("The student has successfully been Updated");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        public void DeleteStudent(int StudentNumber)
        {
            fh.Write($"{StudentNumber}, Deleted, {DateTime.Now}");
            try
            {
                cmd = new SqlCommand("DeleteStudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentNumber", StudentNumber);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student has successfully been removed");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
    }
}
