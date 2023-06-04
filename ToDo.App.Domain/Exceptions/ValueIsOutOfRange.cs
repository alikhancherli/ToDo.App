using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.App.Shared.Exceptions;

namespace ToDo.App.Domain.Exceptions
{
    public class ValueIsOutOfRange : BaseException
    {
        public ValueIsOutOfRange(object range) : base($"The value : [{range}] is not in the correct range.")
        {

        }
    }
}
