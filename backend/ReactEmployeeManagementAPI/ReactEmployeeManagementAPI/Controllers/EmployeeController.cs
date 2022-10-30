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
    public class EmployeeController : ControllerBase
    {
        // Dependency Injection to get Connection String
        private readonly IConfiguration _configuration;

        // Dependency Injection to get application Path into this folder
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // To Get all Employee from Database
        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            /*
              this is raw queries from the tutorial video,
              and should avoid using this.
              Instead, use stored procedures with parameters or
              entity framework to connect to database
             */
            string query = @"SELECT
                                EmployeeId, EmployeeName, Department, CONVERT(VARCHAR(10), DateOfJoining, 120) as DateOfJoining, PhotoFileName
                            
                             FROM
                                dbo.Employee";

            // Creating/Declaring memory for a new DataTable object 
            DataTable table = new DataTable();

            // Getting the connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Declaring memory for SqlDataReader Object 
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

            // Returning the JsonResult with the data table that contains the result of the sql command
            return new JsonResult(table);

        }

        // To Insert a Employee data (object) to Database
        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            /*
                Query statement for Inserting data
             */
            string query = @"INSERT INTO dbo.Employee 
                            (EmployeeName, Department, DateOfJoining, PhotoFileName)
                            VALUES (
                            '" + employee.EmployeeName + @"',
                            '" + employee.Department + @"',
                            '" + employee.DateOfJoining + @"',
                            '" + employee.PhotoFileName + @"'
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


            return new JsonResult("POST Employee successfully operated.");

        }

        // To Update a Employee data (object) to Database
        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            /*
                Query statement for Updating data
             */
            string query = @"
                            UPDATE dbo.Employee set
                            EmployeeName = '" + employee.EmployeeName + @"',
                            Department = '" + employee.Department + @"',
                            DateOfJoining = '" + employee.DateOfJoining + @"'
                            WHERE EmployeeId = " + employee.EmployeeId + @"";

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

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var PhysicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using(var stream = new FileStream(PhysicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (Exception ex)
            {
                return new JsonResult("anonymous.png"); 
            }
        }

        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {
            /*
                this is raw queries from the tutorial video,
                and should avoid using this.
                Instead, use stored procedures with parameters or
                entity framework to connect to database
             */
            string query = @"SELECT
                                DepartmentName
                            
                             FROM
                                dbo.Department";

            // Creating/Declaring memory for a new DataTable object 
            DataTable table = new DataTable();

            // Getting the connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Declaring memory for SqlDataReader Object 
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

            // Returning the JsonResult with the data table that contains the result of the sql command
            return new JsonResult(table);

        }
    }
}
