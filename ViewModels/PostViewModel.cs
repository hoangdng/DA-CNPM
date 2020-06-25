using Microsoft.AspNetCore.Http;
using SemanticWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SemanticWeb.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            DatePosted = DateTime.Now;
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile ImageURL { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }
        public string UserID { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

}