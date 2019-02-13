using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Employees.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Employees.Queries
{
    public class EmployeesWithManagers
    {
        public class Query : IRequest<IEnumerable<EmployeeManagerDto>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<EmployeeManagerDto>>
        {
            private readonly NorthwindDbContext _db;

            public QueryHandler(NorthwindDbContext db)
            {
                _db = db;
            }

            public async Task<IEnumerable<EmployeeManagerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = from emp in _db.Employees
                    join p2 in _db.Employees on emp.ReportsTo equals p2.EmployeeId
                    select new EmployeeManagerDto()
                    {
                        EmployeeId = emp.EmployeeId.ToString(),
                        EmployeeFirstName = emp.FirstName,
                        EmployeeLastName = emp.LastName,
                        EmployeeTitle = emp.Title,
                        ManagerId = p2.EmployeeId.ToString(),
                        ManagerFirstName = p2.FirstName,
                        ManagerLastName = p2.LastName,
                        ManagerTitle = p2.Title

                    };

                var employeesWithManagers = await query.ToListAsync(cancellationToken);
                return employeesWithManagers;

            }
        }
    }
}
