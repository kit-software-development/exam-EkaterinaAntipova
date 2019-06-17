using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RequestServer.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        [Required] // Параметр, укзаывающий, что поле должно быть заполнено = ограничение Not null
        public int Score { get; set; }
        public virtual Film Film { get; set; }
        [Required]
        public int FilmId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
