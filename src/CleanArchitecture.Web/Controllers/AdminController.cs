using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Employees.Commands;
using CleanArchitecture.Application.Employees.Models;
using CleanArchitecture.Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers
{
    [Route("api/admin")]
    public class AdminController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> EmployeeManagerReport()
        {
            var response = await Mediator.Send(new EmployeesWithManagers.Query());

            return Ok(response);
        }

        [HttpPost("changemanager")]
        public async Task<IActionResult> ChangeEmployeeManager([FromBody] ChangeEmployeesManager.Command command)
        {
            var response = await Mediator.Send(command);
            if (!response.Successful)
            {
                return BadRequest(response.Exception.Message);
            }
            return Ok();
        }

    }
}
