using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.Models
{
    public class Post
    {
        public Post()
        {
            PostedDate = DateTime.Now;
            Status = Status.Available;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public Status Status { get; set; }
        public string UserID { get; set; }
    }

    public enum Status
    {
        Available,
        OnHold,
        Solved
    }
}
