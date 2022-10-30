using EmployeeManagementWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // To Get all Departments from Database
        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            /*
              this is raw queries from the tutorial video,
              and should avoid using this.
              Instead, use stored procedures with parameters or
              entity framework to connect to database
             */
            string query = @"SELECT
                                DepartmentId, DepartmentName
                            
                             FROM
                                dbo.Department";

            // Creating/Declaring memory for a new DataTable object 
            DataTable table = new DataTable();

            // Getting the connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Declaring memory for SqlDataReader Object 
            SqlDataReader myReader;

            // Constructing/Creating a new SqlConnection object and using the connection string
            using (SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                // opening the connection string
                myCon.Open();
                // Constructing/Creating a new SqlCommand object with the connection string and the query above
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {   
                    // executing the command and storing it into myReader, SqlDataReader
                    myReader = myCommand.ExecuteReader();

                    // Loading the result of the Sql Command to the data table
                    table.Load(myReader);

                    // Everything is done, now close the SqlDataReader, the result, and the connection string
                    myReader.Close();
                    myCon.Close();

                }
            }

            // Returning the JsonResult with the data table that contains the result of the sql command
            return new JsonResult(table);

        }

        // To Insert a Department data (object) to Database
        [HttpPost]
        public JsonResult Post(Department department)
        {
            /*
                Query statement for Inserting data
             */
            string query = @"INSERT INTO dbo.Department VALUES (
                            '"+department.DepartmentName+@"'
                            )";
            
            // Creating a memory for DataTable
            DataTable table = new DataTable();

            // Getting the connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Creating a memory for SqlDataReader for storing a result of sql command
            SqlDataReader myReader;

            // Constructing/Creating a new SqlConnection object and using the connection string
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                // opening the connection string
                myCon.Open();
                // Constructing/Creating a new SqlCommand object with the connection string and the query above
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    // executing the command and storing it into myReader, SqlDataReader
                    myReader = myCommand.ExecuteReader();

                    // Loading the result of the Sql Command to the data table
                    table.Load(myReader);

                    // Everything is done, now close the SqlDataReader, the result, and the connection string
                    myReader.Close();
                    myCon.Close();

                }
            }


            return new JsonResult("POST Department successfully operated.");

        }

        // To Update a Department data (object) to Database
        [HttpPut]
        public JsonResult Put(Department department)
        {
            /*
                Query statement for Updating data
             */
            string query = @"
                            UPDATE dbo.Department set
                            DepartmentName = '" + department.DepartmentName + @"'
                            WHERE DepartmentId = " + department.DepartmentId + @"";

            // Creating a memory for DataTable
            DataTable table = new DataTable();

            // Getting the connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Creating a memory for SqlDataReader for storing a result of sql command
            SqlDataReader myReader;

            // Constructing/Creating a new SqlConnection object and using the connection string
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                // opening the connection string
                myCon.Open();
                // Constructing/Creating a new SqlCommand object with the connection string and the query above
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    // executing the command and storing it into myReader, SqlDataReader
                    myReader = myCommand.ExecuteReader();

                    // Loading the result of the Sql Command to the data table
                    table.Load(myReader);

                    // Everything is done, now close the SqlDataReader, the result, and the connection string
                    myReader.Close();
                    myCon.Close();

                }
            }


            return new JsonResult("PUT Department successfully operated.");

        }
    }
}
