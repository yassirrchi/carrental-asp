//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentByYassirRchi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservations
    {
        public int Id { get; set; }
        public Nullable<int> CID { get; set; }
        public Nullable<int> VID { get; set; }
        public string Type { get; set; }
    
        public Nullable<System.DateTime> dateDebut { get; set; }
        public Nullable<System.DateTime> dateFin { get; set; }
        public Nullable<int> Cost { get; set; }
        public string Accepted { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Voitures Voitures { get; set; }
    }
}
