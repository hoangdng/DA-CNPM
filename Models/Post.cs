using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Hình ảnh")]
        public string ImageURL { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày đăng")]
        public DateTime PostedDate { get; set; }
        [Display(Name = "Trạng thái")]
        public Status Status { get; set; }
        public string UserID { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserPost> UserPosts { get; set; }
    }

    public enum Status
    {
        Available,
        OnHold,
        Solved
    }
}
