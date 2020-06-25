using SemanticWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SemanticWeb.ViewModels
{
    public class CommentViewModel
    {
        public Post Post { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
