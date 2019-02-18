using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Employees.Commands;
using CleanArchitecture.Application.Infrastructure;
using Xunit;

namespace FunctionalTests.API.Employees
{
    /// <summary>
    /// When changing the values in the DB, we have a dependcy on Northwind Initialize to setup the data.
    /// </summary>
    [Collection("TestFixture")]
    public class ChangeEmployeeManagerTest 
    {
        private HttpClient _client;

        public ChangeEmployeeManagerTest(TestFixture fixture)
        {
            _client = fixture.Client;
        }


        [Fact]
        public async Task Update_ValidManagerId_StatusCodeSuccess()
        {
            //Arrange
            var dto = new ChangeEmployeesManager.Command(1, 3);
            var httpContent = Util.CreateHttpContent(dto);

            //Act
            var response = await _client.PostAsync(Urls.Post.ChangeEmployeeManager, httpContent);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_NoManagerId_StatusCodeSuccess()
        {
            //Arrange
            var dto = new ChangeEmployeesManager.Command(1, null);
            var httpContent = Util.CreateHttpContent(dto);

            //Act
            var response = await _client.PostAsync(Urls.Post.ChangeEmployeeManager, httpContent);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Update_NonExsistentManagerId_StatusCodeError()
        {
            //Arrange
            var dto = new ChangeEmployeesManager.Command(1,999);
            var httpContent = Util.CreateHttpContent(dto);

            //Act
            var response = await _client.PostAsync(Urls.Post.ChangeEmployeeManager, httpContent);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_InvalidEmployeeId_StatusCodeError()
        {
            //Arrange
            var dto = new ChangeEmployeesManager.Command(0, 1);
            var httpContent = Util.CreateHttpContent(dto);

            //Act
            var response = await _client.PostAsync(Urls.Post.ChangeEmployeeManager, httpContent);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
