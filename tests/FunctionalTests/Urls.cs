using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalTests
{
    public class Urls
    {
        private static readonly string BaseUrl = $"/api";

        public static class Get
        {
            public static string EmployeesWithManagers => $"{BaseUrl}/admin";
        }

        public static class Post
        {
            public static string ChangeEmployeeManager => $"{BaseUrl}/admin/changemanager";
        }

    }
}
