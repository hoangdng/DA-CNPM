using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SemanticWeb.Models
{
    public class Post
    {
        public Post()
        {
            DatePosted = DateTime.Now;
        }
        public int Id { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Display(Name = "Hình ảnh")]
        public string ImageURL { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày đăng")]
        public DateTime DatePosted { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Khu vực")]
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
