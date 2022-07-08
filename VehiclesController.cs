using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//changes made by sonali
using System.Net;
using TFLWebApp.DAL;
using TFLWebApp.Models;

namespace TFLWebApp.Controllers
{
    
    public class VehiclesController : Controller
    {
        DbOrmContext entities = new DbOrmContext();
        // GET: Vehicles
        public ActionResult Index()
        {
            int currentVisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = currentVisitCount + 1;

            List<Vehicle> vehicles = entities.Vehicles.ToList();
            this.ViewBag.vehicles = vehicles;
            return View();
        }

        public ActionResult Details(int id)
        {
            int currentVisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = currentVisitCount + 1;

            var vehicle = entities.Vehicles.SingleOrDefault(v => v.Vid == id);
            if (vehicle != null)
            {
                this.ViewData["vehicle"] = vehicle;
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            int currentVisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = currentVisitCount + 1;

            var vehicle = entities.Vehicles.SingleOrDefault(v => v.Vid == id);
            entities.Vehicles.Remove(vehicle ?? throw new InvalidOperationException());
            entities.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Insert()
        {
            int currentVisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = currentVisitCount + 1;

            var vehicle = new Vehicle();
            /*
            if (vehicle != null)
            {
                this.ViewData["vehicle"] = vehicle;
            }
            */
            return View(vehicle);
        }


        [HttpPost]
        public ActionResult Insert(Vehicle vehicle)
        {
            int currentVisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = currentVisitCount + 1;

            if (ModelState.IsValid)
            {
                entities.Vehicles.Add(vehicle);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(vehicle);
          
        }

        public ActionResult Update(int? id)
        {
            int currentVisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = currentVisitCount + 1;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = entities.Vehicles.SingleOrDefault(e => e.Vid == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        [HttpPost]
        public ActionResult Update(Vehicle vehicle)
        {
            int currentVisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = currentVisitCount + 1;

            if (ModelState.IsValid)
            {
                entities.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }
    }
}
