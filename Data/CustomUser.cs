using Microsoft.AspNetCore.Identity;
using PetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.Data
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
        public ICollection<UserPost> UserPosts { get; set; }
    }
}
