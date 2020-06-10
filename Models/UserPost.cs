using PetWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.Models
{
    public class UserPost
    {
        public string CustomUserId { get; set; }
        public CustomUser CustomUser { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
