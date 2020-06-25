using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SemanticWeb.Models
{
    public class Comment
    {
        public Comment()
        {
            DateCommented = DateTime.Now;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCommented { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
