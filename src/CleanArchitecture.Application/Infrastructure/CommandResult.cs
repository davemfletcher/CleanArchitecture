using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Infrastructure
{
    public class CommandResult
    {
        public bool Successful { get; private set; }

        public Exception Exception { get; private set; }

        public static CommandResult Success()
        {
            return new CommandResult { Successful = true };
        }

        public static CommandResult Success(dynamic objectId)
        {
            return new CommandResult { Successful = true, ObjectId = objectId };
        }

        private dynamic ObjectId { get; set; }

        public static CommandResult Error(Exception exception)
        {
            return new CommandResult { Successful = false, Exception = exception };
        }
    }
}
