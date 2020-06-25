using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemanticWeb.Data
{
    public class CustomUser : IdentityUser
    {
        public CustomUser()
        {
            DateJoined = DateTime.Now;
        }

        [PersonalData]
        public string Name { get; set; }

        [PersonalData]
        public DateTime DateJoined { get; set; }
    }
}
