using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie.Data.Sql;
using Movie.Web.Mvc.Models;
using Nile.Data;

namespace Movie.Web.Mvc.Controllers
{
    public class MoviesController : Controller
    {
        public MoviesController()
        {
            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"];
            _database = new SqlMovieDatabase(connString.ConnectionString);
        }
        private readonly IMovieDatabase _database;

        [HttpGet]
        public ActionResult Index()
        {
            var movies = _database.GetAll();

        return View(movies.Select(m => m.ToModel()));
        }

        [HttpGet]
        public ActionResult create()
        {
            return View(new MovieModel());
        }

        [HttpPost]
        public ActionResult Create ( MovieModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var movie = model.ToDomain();
                    movie = _database.Add(movie);
                    return RedirectToAction(nameof(Index));
                };
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit (int id)
        {
            var movie = _database.GetAll().FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie.ToModel());
        }

        [HttpPost]
        public ActionResult Edit( MovieModel model )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var movie = model.ToDomain();
                    movie = _database.Add(movie);
                    return RedirectToAction("Index");
                };
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };
            return View(model);
        }

        [HttpGet]
        [Route("products/delete/{id}")]
        public ActionResult Delete(int id)
        {
            var movie = _database.GetAll().FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie.ToModel());
        }

        [HttpPost]
        public ActionResult Delete (MovieModel model)
        {
            try
            {
                var movie = _database.GetAll().FirstOrDefault(m => m.Id == model.Id);
                if (movie == null)
                    return HttpNotFound();
                _database.Remove(model.Id);

                return RedirectToAction(nameof(Index));
            }catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };
            return View(model);
        }









    }
}