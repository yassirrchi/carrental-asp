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
    
    public partial class Voitures
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voitures()
        {
            this.Reservations = new HashSet<Reservations>();
        }
    
        public int Id { get; set; }
        public string Marque { get; set; }
        public string Categorie { get; set; }
        public string Serie { get; set; }
        public string Immatriculaion { get; set; }
        public Nullable<System.DateTime> DateMC { get; set; }
        public string Carburant { get; set; }
        public Nullable<int> PrixJ { get; set; }
        public string Dispo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}