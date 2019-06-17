using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestServer.Models;

namespace RequestServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [Route("AddFilm")]
        public ActionResult AddFilm(Film film)
        { 
            if (!ModelState.IsValid)
                return BadRequest();
            using(var dbHelper = new DbModel())
            {
                dbHelper.Films.Add(film);
                dbHelper.SaveChanges();
                return StatusCode(200, film.Id);  //код 200 - "OK"
            }
        }

        [Route("RemoveFilm")]
        public ActionResult RemoveFilm(int Id)
        {
            using (var dbHelper = new DbModel())
            {
                var film = dbHelper.Films.FirstOrDefault(x => x.Id == Id);
                if (film == null)
                    return BadRequest();
                dbHelper.Films.Remove(film);
                dbHelper.Comments.RemoveRange(dbHelper.Comments.Where(x => x.FilmId == Id));
                dbHelper.Assessments.RemoveRange(dbHelper.Assessments.Where(x => x.FilmId == Id));

                dbHelper.SaveChanges();
                return StatusCode(200);
            }
        }
    }
}