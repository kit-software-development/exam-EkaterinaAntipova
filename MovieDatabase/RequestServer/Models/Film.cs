using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RequestServer.Models
{
    public class Film
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public double Score { get; set; }
        public int ScoreCount { get; set; }
        public string ImagePath { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
