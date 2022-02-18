using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentByYassirRchi.Models;

namespace CarRentByYassirRchi.Controllers
{
    public class AgentController : Controller
    {
        private CarRentEntities db = new CarRentEntities();

        // GET: Agent
        public ActionResult Login()
        {
            Session["AgentEmail"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")] Staff staff)
        {
            var r = db.Staff.Where(x => x.Email == staff.Email && x.Password == staff.Password).ToList().FirstOrDefault();

            if (r == null)
            {
                ViewBag.loginErr = "mdp ou email non correct";
                return View(staff);
            }
            else if (r.Function == "agent" && r != null)
            {
                Session["AgentEmail"] = r.Email;
                return RedirectToAction("Clients");
            }
            else
            {
                ViewBag.loginErr = "";
                return View(staff);
            }

        }
        public ActionResult Index()
        {
            if (Session["AgentEmail"] != null)
               
            return View(db.Staff.ToList());

        return RedirectToAction("Login"); }

      

  
 
        public ActionResult EditClient(int? id)
        {
            
            if (Session["AgentEmail"] != null) {
               
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients); }

 return RedirectToAction("Login");
        }
        //Post editclient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient([Bind(Include = "Id,Nom,Prenom,AdresseElec,NumTel,DateNais,CIN,Permis,Password,EtatdeCompte")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Clients");
            }
            return View(clients);
        }

         
        public ActionResult Clients()
        {
            //affichge des client
            if (Session["AgentEmail"] != null)
               
            return View(db.Clients.ToList());
            return RedirectToAction("Login");
        }
        
        public ActionResult Reservations()
        {
            //affichage des reserv
            if (Session["AgentEmail"] != null) {

                var reservations = db.Reservations.Include(r => r.Clients).Include(r => r.Voitures);
            return View( reservations.ToList()); }
            return RedirectToAction("Login");
        }
        public ActionResult EditReservation(int? id)
        {
            // modif  des reserv
            if (Session["AgentEmail"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservations reservations = db.Reservations.Find(id);
                if (reservations == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CID = new SelectList(db.Clients, "Id", "Nom", reservations.CID);
                ViewBag.VID = new SelectList(db.Voitures, "Id", "Marque", reservations.VID);
                return View(reservations);
            }
            return RedirectToAction("Login");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReservation([Bind(Include = "Id,CID,VID,Type,dateDebut,dateFin,Cost,Accepted")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Reservations");
            }
            ViewBag.CID = new SelectList(db.Clients, "Id", "Nom", reservations.CID);
            ViewBag.VID = new SelectList(db.Voitures, "Id", "Marque", reservations.VID);
            return View(reservations);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staff.Find(id);
            db.Staff.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
