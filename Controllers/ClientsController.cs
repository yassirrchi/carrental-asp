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
    public class ClientsController : Controller
    {
        private CarRentEntities db = new CarRentEntities();

        // GET: Clients
        public ActionResult Index()
        {
            if(Session["ClientId"] !=null)
            return View(db.Voitures.ToList());
            return RedirectToAction("Login");
        }
         
        public ActionResult Signup()
        {
            
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "Id,Nom,Prenom,AdresseElec,NumTel,DateNais,CIN,Permis,Password,EtatdeCompte")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(clients);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(clients);
        }

        public ActionResult Login()
        {Session["ClientEmail"] = null;
            Session["ClientId"] = null;
            //affichge des client
            return View();

        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "AdresseElec,Password")] Clients clients)
        {
            var r = db.Clients.Where(x => x.AdresseElec == clients.AdresseElec && x.Password == clients.Password).ToList().FirstOrDefault();

            if (r == null)
            {
                ViewBag.loginErr = "mdp ou email non correct";
                return View(clients);
            }
            else if ( r != null && r.EtatdeCompte== "Active    ")
            {
                Session["ClientEmail"] = r.AdresseElec;
                Session["ClientId"] = r.Id;
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.loginErr = "";
                return View(clients);
            }

        }
         
          public ActionResult Reserver(int? id)
        {
            if (Session["ClientId"] != null) { 
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var voitureData = db.Voitures.Where(x => x.Id == id).ToList().FirstOrDefault();

            ViewBag.CID = Session["ClientId"];
            ViewBag.VID = voitureData.Id;
            ViewBag.Marque = voitureData.Marque;
            ViewBag.Serie = voitureData.Serie;
            ViewBag.PrixJ = voitureData.PrixJ;

            return View();
            }
            return RedirectToAction("Login");
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserver([Bind(Include = "Id,CID,VID,Type,dateDebut,dateFin,Cost,Accepted")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.CID = new SelectList(db.Clients, "Id", "Nom", reservations.CID);
            ViewBag.VID = new SelectList(db.Voitures, "Id", "Marque", reservations.VID);
            return View(reservations);
        } 

        public ActionResult MyReservations()
        {
            if (Session["ClientId"] != null)
            { 

                int clientIdHolder = Convert.ToInt32(Session["ClientId"]);
            var mesRes=db.Reservations.Where(x => x.CID==clientIdHolder ).ToList();
            if (mesRes == null)
                return RedirectToAction("Voitures");
            else

            return View(mesRes);}
            return RedirectToAction("Login");
        }
        public ActionResult DeleteReservation(int? id)
        {
            if (Session["ClientId"] != null) { 

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
            return RedirectToAction("MyReservations");
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = db.Clients.Find(id);
            db.Clients.Remove(clients);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditReservation(int? id)
        {
            // modif  des reserv
            if (Session["ClientEmail"] != null) { 
           // {
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
          //  }
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
