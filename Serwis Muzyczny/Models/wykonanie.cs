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
    
    public partial class wykonanie
    {
        public int wykonanieId { get; set; }
        public int artystaId { get; set; }
        public int utworId { get; set; }
    
        public virtual artysta artysta { get; set; }
        public virtual utwor utwor { get; set; }
    }
}
