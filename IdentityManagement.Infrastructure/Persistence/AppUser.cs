using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityManagement.Infrastructure.Persistence
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
    }
}