using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Employees.Queries;
using Xunit;

namespace IntegrationTests.Employees
{
    [Collection("TestFixture")]
    public class GetEmployeesWithManagersTest
    {

        [Fact]
        public async Task GetList_EmployyesWithManagers_Pass()
        {
            //since this class is all gets, probably extract db into a class property
            using (var db = DbFactory.Create())
            {
                //Arrange
                var sut = new EmployeesWithManagers.QueryHandler(db);
                var query = new EmployeesWithManagers.Query();

                //Act
                var listOfStaff = await sut.Handle(query, CancellationToken.None);
                var count = listOfStaff.ToList().Count;

                //Assert
                Assert.True(count == 8, $"Actual count is {count}");
            }

        }

    }
}
