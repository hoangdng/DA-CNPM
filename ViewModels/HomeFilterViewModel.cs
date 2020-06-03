using PetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.ViewModels
{
    public class HomeFilterViewModel
    {
        public List<Post> Posts { get; set; }
        public int CategoryId { get; set; }
    }
}
