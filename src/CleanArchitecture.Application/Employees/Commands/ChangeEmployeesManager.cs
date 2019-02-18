using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Infrastructure;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Employees.Commands
{
    public class ChangeEmployeesManager
    {

        public ChangeEmployeesManager()
        {
            
        }
        //ChangeEmployeesManagerCommand
        public class Command : IRequest<CommandResult>
        {
            public int EmployeeId { get; private set; }
            public int? ManagerId { get; private set; }

            public Command(int employeeId, int? managerId)
            {
                EmployeeId = employeeId;
                ManagerId = managerId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.EmployeeId).NotEmpty();
                RuleFor(x => x.EmployeeId).GreaterThan(0);
            }
        }

        public class CommandHandler : IRequestHandler<Command, CommandResult>
        {
            private readonly NorthwindDbContext _db;

            public CommandHandler(NorthwindDbContext db)
            {
                _db = db;
            }

            public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {
                //check the employee and/or manager exists
                var existingEmployee = await _db.Employees.FirstOrDefaultAsync(i => i.EmployeeId == request.EmployeeId, cancellationToken);
                Employee existingManager = null;

                if (existingEmployee == null)
                {
                    return CommandResult.Error(new NotFoundException(nameof(Employee), request.EmployeeId.ToString()));
                }

                //check manager exists
                if (request.ManagerId != null && request.ManagerId > 0)
                {
                    existingManager = await _db.Employees.FirstOrDefaultAsync(i => i.EmployeeId == request.ManagerId, cancellationToken);
                    if (existingManager == null)
                    {
                        return CommandResult.Error(new NotFoundException(nameof(Employee), $"Manager: {request.ManagerId.ToString()}"));
                    }
                }

                existingEmployee.Manager = existingManager;
                _db.Employees.Update(existingEmployee);
                await _db.SaveChangesAsync(cancellationToken);

                return CommandResult.Success();

            }
        }
    }
}
