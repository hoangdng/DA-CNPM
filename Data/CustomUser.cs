using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.Data
{
    public class CustomUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
    }
}
