using PetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public DateTime DateJoined { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
