using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RequestServer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual Film Film { get; set; }
        [Required]
        public int FilmId { get; set; }
        public DateTime Date { get; set; }
    }
}
