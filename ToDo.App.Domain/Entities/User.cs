using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.App.Domain.Entities
{
    public sealed class User: IdentityUser<int>
    {
        public string FullName { get; private set; } = default!;
        public bool IsActive { get; private set; }
    }
}
