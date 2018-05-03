using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nile.Data;
using Nile.Data.Sequel;
using Nile.Web.Mvc.Models;

namespace Nile.Web.Mvc.Content
{
    public class ProductsController : Controller
    {
        private readonly IProductDatabase _database;
        public ProductsController()
        {
            var connString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=NileDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
            _database = new sqlProductDatabase(connString);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var products = _database.GetAll();

            return View(products.Select(p => p.ToModel()));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = model.ToDomain();

                    product = _database.Add(product);
                    return RedirectToAction(nameof(Index));                    
                }
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit (int id)
        {
            var product = _database.GetAll().FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product.ToModel());
        }
        [HttpPost]
        public IActionResult Edit( ProductModel model )
        {
            try
            {
                    if (ModelState.IsValid)
                    {
                        var product = model.ToDomain();

                        product = _database.Update(product);
                    return RedirectToAction("Index");
                    }
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };

            return View(model);
        }

        [HttpGet]
        [Route("products/delete/{id}")]
        public ActionResult Delete( int id )
        {
            var product = _database.GetAll().FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product.ToModel());
        }
        [HttpPost]
        public IActionResult Delete( ProductModel model )
        {
            try
            {
                var product = _database.GetAll().FirstOrDefault(p => p.Id == model.Id);
                if (product == null)
                    return NotFound();

                _database.Remove(model.Id);
                return RedirectToAction(nameof(Index));
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };

            return View(model);
        }
    }
}