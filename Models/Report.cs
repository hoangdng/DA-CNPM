using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.Models
{
    public class Report
    {
        public Report()
        {
            DateReported = DateTime.Now;
        }
        public int Id { get; set; }
        [Display(Name = "Nội dung report")]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày report")]
        public DateTime DateReported { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
