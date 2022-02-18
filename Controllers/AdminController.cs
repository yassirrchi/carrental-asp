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
    public class AdminController : Controller
    {
        private CarRentEntities db = new CarRentEntities();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["AdminEmail"] != null) 
            return View(db.Staff.ToList());
            return RedirectToAction("Login");
        }

  
  
        public ActionResult Clients()
        {
            //affichge des client
            if (Session["AdminEmail"] != null) { 
            return View(db.Clients.ToList());}
            return RedirectToAction("Login");

        }
        public ActionResult CreateClient() {
            //creation d'un client
            if (Session["AdminEmail"] != null) { 
                return View();}
            return RedirectToAction("Login");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClient([Bind(Include = "Id,Nom,Prenom,AdresseElec,NumTel,DateNais,CIN,Permis,Password,EtatdeCompte")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(clients);
                db.SaveChanges();
                return RedirectToAction("Clients");
            }

            return View(clients);
        }
       
        public ActionResult EditClient(int? id)
        {
            if (Session["AdminEmail"] != null) { 
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);}
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
        public ActionResult DeleteClient(int? id)
        {
            if (Session["AdminEmail"] != null) {
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
        [HttpPost,ActionName("DeleteClient")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClientPost(int id)
        {
            Clients clients = db.Clients.Find(id);
            db.Clients.Remove(clients);
            db.SaveChanges();
            return RedirectToAction("Clients");
        }

        public ActionResult Reservations()
        {
            if (Session["AdminEmail"] != null) { 
                var reservations = db.Reservations.Include(r => r.Clients).Include(r => r.Voitures);
            return View(reservations.ToList()); }
            return RedirectToAction("Login");

        }
        
        
           public ActionResult EditReservation(int? id)
        {
            if (Session["AdminEmail"] != null) { 
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
            return View(reservations);}
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
        public ActionResult DeleteReservation(int? id)
        {
            if (Session["AdminEmail"] != null) { 

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);}
            return RedirectToAction("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReservation(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            db.Reservations.Remove(reservations);
            db.SaveChanges();
            return RedirectToAction("Reservations");
        }
       
        public ActionResult Login()
        {
            Session["AdminEmail"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")] Staff staff)
        {
            var r = db.Staff.Where(x=>x.Email==staff.Email && x.Password==staff.Password ).ToList().FirstOrDefault();

            if (r == null) {
                ViewBag.loginErr = "mdp ou email non correct";
                return View(staff);
            }
          else  if (r.Function == "admin" && r != null)
            {
                Session["AdminEmail"] = r.Email;
                return RedirectToAction("Clients");
            }
            else {
                ViewBag.loginErr = "";
                return View(staff);
            }
             
        }
        public ActionResult Voitures()
        {
            if (Session["AdminEmail"] != null) { 
                return View(db.Voitures.ToList());}
            return RedirectToAction("Login");

        }
        public ActionResult AjouterVoiture()
        {
            if (Session["AdminEmail"] != null) {
            return View();
            }
                return RedirectToAction("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjouterVoiture([Bind(Include = "Id,Marque,Categorie,Serie,Immatriculaion,DateMC,Carburant,PrixJ,Dispo")] Voitures voitures)
        {
            if (ModelState.IsValid)
            {
                db.Voitures.Add(voitures);
                db.SaveChanges();
                return RedirectToAction("Voitures");
            }

            return View(voitures);
        }

        public ActionResult EditVoiture(int? id)
        {
            if (Session["AdminEmail"] != null) { 
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voitures voitures = db.Voitures.Find(id);
            if (voitures == null)
            {
                return HttpNotFound();
            }
            return View(voitures);}
            return RedirectToAction("Login");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVoiture([Bind(Include = "Id,Marque,Categorie,Serie,Immatriculaion,DateMC,Carburant,PrixJ,Dispo")] Voitures voitures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voitures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Voitures");
            }
            return View(voitures);
        }
     
        public ActionResult DeleteVoiture(int? id)
        {
            if (Session["AdminEmail"] != null) { 
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voitures voitures = db.Voitures.Find(id);
            if (voitures == null)
            {
                return HttpNotFound();
            }
            return View(voitures);}
            return RedirectToAction("Login");
        }
        [HttpPost, ActionName("DeleteVoiture")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voitures voitures = db.Voitures.Find(id);
            db.Voitures.Remove(voitures);
            db.SaveChanges();
            return RedirectToAction("Voitures");
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
