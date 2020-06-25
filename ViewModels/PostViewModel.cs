using Microsoft.AspNetCore.Http;
using PetWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            PostedDate = DateTime.Now;
            Status = Status.Available;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageURL { get; set; }
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; }
        public Status Status { get; set; }
        public string UserID { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

}