using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models2;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ContractController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get_Id(int Id)
        {
            string query = @"
                select ContractId as ""ContractId"",
                        ContractName as ""ContractName"",
                        ContractEmail as ""ContractEmail"",
                        ContractPhone as ""ContractPhone"",
                        ContractIBAN as ""ContractIBAN"",
                        to_char(ContractStartDate,'YYYY-MM-DD') as ""ContractStartDate"",
                        to_char(ContractDueDate,'YYYY-MM-DD') as ""ContractDueDate""
                      
                        
                from Contract
                where ContractId = @id;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Close();
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            if (table.Rows.Count == 0)
            {
                return NotFound();
            }

            return new JsonResult(table);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string sort_by = null, string sort_type = "asenv", string value = null)
        {
            string query = @"
                select ContractId as ""ContractId"",
                        ContractName as ""ContractName"",
                        ContractEmail as ""ContractEmail"",
                        ContractPhone as ""ContractPhone"",
                        ContractIBAN as ""ContractIBAN"",
                        to_char(ContractStartDate,'YYYY-MM-DD') as ""ContractStartDate"",
                        to_char(ContractDueDate,'YYYY-MM-DD') as ""ContractDueDate""
                        
                        
                from Contract;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Close();
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }


            if (value != null)
            {
                string filter = $"(ContractId,ContractName+ContractEmail+ContractPhone+ContractIBAN+ContractStartDate+ContractDueDate) like '%{value}%'";
                DataRow[] rows = table.Select(filter);
                DataTable data = table.Clone();
                foreach (DataRow row in rows)
                {
                    data.ImportRow(row);
                }
                data.AcceptChanges();
                table = data;
            }
            if (sort_by != null)
            {
                DataTable temp = table.Clone();
                foreach (DataRow dr in table.Rows)
                {
                    temp.ImportRow(dr);
                }
                temp.AcceptChanges();

                DataView dv = temp.DefaultView;
                dv.Sort = $"{sort_by} {sort_type.ToUpper()}";
                table = dv.ToTable();
            }
            if (table.Rows.Count == 0)
            {
                return NotFound();
            }
            
            return new JsonResult(table);
        }
       

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(Contract emp)
        {
            JsonResult errors;
            if (Validation.isValid(emp, out errors))
            {

                string query = @"
                    insert into Contract (ContractName,ContractEmail,ContractPhone,ContractIBAN,ContractStartDate,ContractDueDate) 
                    values               (@ContractName,@ContractEmail,@ContractPhone,@ContractIBAN,@ContractStartDate,@ContractDueDate) 
                ";

                
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {


                        myCommand.Parameters.AddWithValue("@ContractName", emp.ContractName);
                        myCommand.Parameters.AddWithValue("@ContractEmail", emp.ContractEmail);
                        myCommand.Parameters.AddWithValue("@ContractPhone", emp.ContractPhone);
                        myCommand.Parameters.AddWithValue("@ContractIBAN", emp.ContractIBAN);
                        myCommand.Parameters.AddWithValue("@ContractStartDate", Convert.ToDateTime(emp.ContractStartDate));
                        myCommand.Parameters.AddWithValue("@ContractDueDate", Convert.ToDateTime(emp.ContractDueDate));
                        

                        myReader = myCommand.ExecuteReader();
                        

                        myReader.Close();
                        myCon.Close();

                    }
                }
                return new JsonResult("Added Successfully");
            }

            return BadRequest(errors);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(Contract emp)
        {
            JsonResult errors;
            if (Validation.isValid(emp, out errors))
            {
                string query = @"
                        update Contract

                        set
                        ContractName = @ContractName,
                        ContractEmail = @ContractEmail,
                        ContractPhone = @ContractPhone,
                        ContractIBAN = @ContractIBAN,
                        ContractStartDate = @ContractStartDate,
                        ContractDueDate = @ContractDueDate
                        
                        where ContractId=@ContractId;
                    ";

                
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@ContractId", emp.ContractId);
                        myCommand.Parameters.AddWithValue("@ContractName", emp.ContractName);
                        myCommand.Parameters.AddWithValue("@ContractEmail", emp.ContractEmail);
                        myCommand.Parameters.AddWithValue("@ContractPhone", emp.ContractPhone);
                        myCommand.Parameters.AddWithValue("@ContractIBAN", emp.ContractIBAN);
                        myCommand.Parameters.AddWithValue("@ContractStartDate", Convert.ToDateTime(emp.ContractStartDate));
                        myCommand.Parameters.AddWithValue("@ContractDueDate", Convert.ToDateTime(emp.ContractDueDate));
                        myReader = myCommand.ExecuteReader();
                        

                        myReader.Close();
                        myCon.Close();

                    }
                }
                return new JsonResult("Updated Successfully");
            }
            return BadRequest(errors);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"
                delete from Contract
                where ContractId=@ContractId 
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContractId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");
        }




    }
}