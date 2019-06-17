using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestServer.Models;

namespace RequestServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("Registration")]
        public ActionResult Registration(User user)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(user.NickName))
                return BadRequest();
            using(var dbHelper = new DbModel())
            {
                dbHelper.Users.Add(user);
                dbHelper.SaveChanges();
            }

            return StatusCode(200, user.Id);
        }

        [HttpPost]
        [Route("LogIn")]
        public ActionResult LogIn(User user)
        {
            using(var dbHelper = new DbModel())
            {
                var us = dbHelper.Users.FirstOrDefault(x => x.Login == user.Login && x.Password == user.Password);
                if (us == null)
                    return BadRequest();
                return new JsonResult(us);
            }
        }

        [HttpPost]
        [Route("AddComment")]
        public ActionResult AddComment(Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            using(var dbHelper = new DbModel())
            {
                dbHelper.Comments.Add(comment);
                dbHelper.SaveChanges();
            }
            return new JsonResult(comment);
        }

        [HttpPost]
        [Route("RemoveComment")]
        public ActionResult RemoveComment(int Id)
        {
            using (var dbHelper = new DbModel())
            {
                var comment = dbHelper.Comments.FirstOrDefault(x => x.Id == Id);
                if (comment == null)
                    return BadRequest();
                dbHelper.Comments.Remove(comment);
                dbHelper.SaveChanges();
            }
            return StatusCode(200);
        }

        [HttpPost]
        [Route("AddAssessment")]
        public ActionResult AddAssessment(Assessment assessment)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            using(var dbHelper = new DbModel())
            {
                if(assessment.Id == 0)
                {
                    var film = dbHelper.Films.First(x => x.Id == assessment.FilmId);
                    film.Score = (film.Score * film.ScoreCount + assessment.Score) / ++film.ScoreCount;
                    dbHelper.Assessments.Add(assessment);
                }
                else
                {
                    var oldAssess = dbHelper.Assessments.FirstOrDefault(x => x.Id == assessment.Id);
                    var film = dbHelper.Films.First(x => x.Id == assessment.FilmId);
                    film.Score = (film.Score * film.ScoreCount - oldAssess.Score + assessment.Score) / film.ScoreCount;
                    dbHelper.Assessments.Add(assessment);
                }
                dbHelper.SaveChanges();
                return StatusCode(200, assessment.Id);
            }
        }

        [HttpGet]
        [Route("GetFilms")] 
        public ActionResult GetFilms()
        {
            using(var dbHelper = new DbModel())
            {
                var films = dbHelper.Films.ToList();
                return new JsonResult(films);
            }
        }

        [HttpGet]
        [Route("GetComments")]
        public ActionResult GetComments(int id)
        {
            using (var dbHelper = new DbModel())
            {
                var comments = dbHelper.Comments.Include(x => x.User).Where(x => x.FilmId == id)
                    .Select(x => new
                    {
                        x.UserId,
                        x.Text,
                        x.Date,
                        User = new
                        {
                            NickName = x.User.NickName
                        }
                    }).OrderByDescending(x=> x.Date).ToList();
                return new JsonResult(comments);
            }
        }
    }
}