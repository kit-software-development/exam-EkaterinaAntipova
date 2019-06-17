using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestServer.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public virtual Film Film { get; set; }
        public int FilmId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
