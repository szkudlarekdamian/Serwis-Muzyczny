//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Serwis_Muzyczny.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class odsluch
    {
        [DisplayName("Id odsłuchu")]
        public int odsluchId { get; set; }
        [DisplayName("Id użytkownika")]
        public string uzytkownikId { get; set; }
        [DisplayName("Id utworu")]
        public int utworId { get; set; }
        [DisplayName("Data odtworzenia")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime dataOdtworzenia { get; set; }
    
        public virtual utwor utwor { get; set; }
        public virtual uzytkownik uzytkownik { get; set; }
    }
}
