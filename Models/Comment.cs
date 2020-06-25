using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.Models
{
    public class Comment
    {
        public Comment()
        {
            DateComment = DateTime.Now;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateComment { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
