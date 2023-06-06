using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.App.Infrastructure.Persistence
{
    public interface ISeedData
    {
        Task Seed();
    }
}
