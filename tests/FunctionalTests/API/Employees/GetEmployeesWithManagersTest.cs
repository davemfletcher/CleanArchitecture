using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Web;
using Xunit;

namespace FunctionalTests.API.Employees
{

    /// <summary>
    /// Not a great test as it not passing any data through, will make more sense on get with params, posts and patch.
    /// </summary>
    [Collection("TestFixture")]
    public class GetEmployeesWithManagersTest 
    {
        private readonly HttpClient _client;

        public GetEmployeesWithManagersTest(TestFixture fixture)
        {
            _client = fixture.Client;
        }


        [Fact]
        public async Task GetList_EmployyesWithManagers_Pass()
        {
            var response = await _client.GetAsync(Urls.Get.EmployeesWithManagers);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
