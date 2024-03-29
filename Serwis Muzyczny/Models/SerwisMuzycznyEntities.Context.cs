﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SerwisMuzycznyEntities : DbContext
    {
        public SerwisMuzycznyEntities()
            : base("name=SerwisMuzycznyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<album> album { get; set; }
        public virtual DbSet<artysta> artysta { get; set; }
        public virtual DbSet<gatunek> gatunek { get; set; }
        public virtual DbSet<odsluch> odsluch { get; set; }
        public virtual DbSet<planUzytkownik> planUzytkownik { get; set; }
        public virtual DbSet<plany> plany { get; set; }
        public virtual DbSet<przynaleznosc> przynaleznosc { get; set; }
        public virtual DbSet<utwor> utwor { get; set; }
        public virtual DbSet<uzytkownik> uzytkownik { get; set; }
        public virtual DbSet<wykonanie> wykonanie { get; set; }
    
        public virtual int dodaj_album(string nazwa, Nullable<System.DateTime> data, string nazwa_gatunku, string pseudonim)
        {
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(string));
    
            var dataParameter = data.HasValue ?
                new ObjectParameter("data", data) :
                new ObjectParameter("data", typeof(System.DateTime));
    
            var nazwa_gatunkuParameter = nazwa_gatunku != null ?
                new ObjectParameter("nazwa_gatunku", nazwa_gatunku) :
                new ObjectParameter("nazwa_gatunku", typeof(string));
    
            var pseudonimParameter = pseudonim != null ?
                new ObjectParameter("pseudonim", pseudonim) :
                new ObjectParameter("pseudonim", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_album", nazwaParameter, dataParameter, nazwa_gatunkuParameter, pseudonimParameter);
        }
    
        public virtual int dodaj_artyste(string pseudonim)
        {
            var pseudonimParameter = pseudonim != null ?
                new ObjectParameter("pseudonim", pseudonim) :
                new ObjectParameter("pseudonim", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_artyste", pseudonimParameter);
        }
    
        public virtual int dodaj_gatunek(string nazwa)
        {
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_gatunek", nazwaParameter);
        }
    
        public virtual int dodaj_odsluch(string uztkownikId, Nullable<int> utworId)
        {
            var uztkownikIdParameter = uztkownikId != null ?
                new ObjectParameter("uztkownikId", uztkownikId) :
                new ObjectParameter("uztkownikId", typeof(string));
    
            var utworIdParameter = utworId.HasValue ?
                new ObjectParameter("utworId", utworId) :
                new ObjectParameter("utworId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_odsluch", uztkownikIdParameter, utworIdParameter);
        }
    
        public virtual int dodaj_plan_uzytkownik_transakcja(string uzytkownikId, Nullable<int> planId)
        {
            var uzytkownikIdParameter = uzytkownikId != null ?
                new ObjectParameter("uzytkownikId", uzytkownikId) :
                new ObjectParameter("uzytkownikId", typeof(string));
    
            var planIdParameter = planId.HasValue ?
                new ObjectParameter("planId", planId) :
                new ObjectParameter("planId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_plan_uzytkownik_transakcja", uzytkownikIdParameter, planIdParameter);
        }
    
        public virtual int dodaj_przynaleznosc(Nullable<int> albumId, Nullable<int> utworId)
        {
            var albumIdParameter = albumId.HasValue ?
                new ObjectParameter("albumId", albumId) :
                new ObjectParameter("albumId", typeof(int));
    
            var utworIdParameter = utworId.HasValue ?
                new ObjectParameter("utworId", utworId) :
                new ObjectParameter("utworId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_przynaleznosc", albumIdParameter, utworIdParameter);
        }
    
        public virtual int dodaj_utwor(string nazwa, string dlugosc)
        {
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(string));
    
            var dlugoscParameter = dlugosc != null ?
                new ObjectParameter("dlugosc", dlugosc) :
                new ObjectParameter("dlugosc", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_utwor", nazwaParameter, dlugoscParameter);
        }
    
        public virtual int dodaj_uzytkownika(string nick, string imie, string email, string nazwisko, string kraj, Nullable<System.DateTime> dataUrodzenia, string miejscowosc, string rodzajMiejscowosci, string plec)
        {
            var nickParameter = nick != null ?
                new ObjectParameter("nick", nick) :
                new ObjectParameter("nick", typeof(string));
    
            var imieParameter = imie != null ?
                new ObjectParameter("imie", imie) :
                new ObjectParameter("imie", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var nazwiskoParameter = nazwisko != null ?
                new ObjectParameter("nazwisko", nazwisko) :
                new ObjectParameter("nazwisko", typeof(string));
    
            var krajParameter = kraj != null ?
                new ObjectParameter("kraj", kraj) :
                new ObjectParameter("kraj", typeof(string));
    
            var dataUrodzeniaParameter = dataUrodzenia.HasValue ?
                new ObjectParameter("dataUrodzenia", dataUrodzenia) :
                new ObjectParameter("dataUrodzenia", typeof(System.DateTime));
    
            var miejscowoscParameter = miejscowosc != null ?
                new ObjectParameter("miejscowosc", miejscowosc) :
                new ObjectParameter("miejscowosc", typeof(string));
    
            var rodzajMiejscowosciParameter = rodzajMiejscowosci != null ?
                new ObjectParameter("rodzajMiejscowosci", rodzajMiejscowosci) :
                new ObjectParameter("rodzajMiejscowosci", typeof(string));
    
            var plecParameter = plec != null ?
                new ObjectParameter("plec", plec) :
                new ObjectParameter("plec", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_uzytkownika", nickParameter, imieParameter, emailParameter, nazwiskoParameter, krajParameter, dataUrodzeniaParameter, miejscowoscParameter, rodzajMiejscowosciParameter, plecParameter);
        }
    
        public virtual int dodaj_wykonanie(Nullable<int> artystaId, Nullable<int> utworId)
        {
            var artystaIdParameter = artystaId.HasValue ?
                new ObjectParameter("artystaId", artystaId) :
                new ObjectParameter("artystaId", typeof(int));
    
            var utworIdParameter = utworId.HasValue ?
                new ObjectParameter("utworId", utworId) :
                new ObjectParameter("utworId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodaj_wykonanie", artystaIdParameter, utworIdParameter);
        }
    
        public virtual int dodajPlan(Nullable<int> iloscPiosenek, Nullable<decimal> cena, string nazwa)
        {
            var iloscPiosenekParameter = iloscPiosenek.HasValue ?
                new ObjectParameter("iloscPiosenek", iloscPiosenek) :
                new ObjectParameter("iloscPiosenek", typeof(int));
    
            var cenaParameter = cena.HasValue ?
                new ObjectParameter("cena", cena) :
                new ObjectParameter("cena", typeof(decimal));
    
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodajPlan", iloscPiosenekParameter, cenaParameter, nazwaParameter);
        }
    
        public virtual int dodajPlanUzytkownik(string idUzytkowni, Nullable<int> idPlan)
        {
            var idUzytkowniParameter = idUzytkowni != null ?
                new ObjectParameter("idUzytkowni", idUzytkowni) :
                new ObjectParameter("idUzytkowni", typeof(string));
    
            var idPlanParameter = idPlan.HasValue ?
                new ObjectParameter("idPlan", idPlan) :
                new ObjectParameter("idPlan", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dodajPlanUzytkownik", idUzytkowniParameter, idPlanParameter);
        }
    
        public virtual int edycja_uzytkownik(string uzytkownikId, string imie, string nazwisko, string email, string kraj, Nullable<System.DateTime> dataUrodzenia, string miejscowosc, string rodzajMiejscowosci, string plec, Nullable<int> pozostalaIlosc, string dataEdycji)
        {
            var uzytkownikIdParameter = uzytkownikId != null ?
                new ObjectParameter("uzytkownikId", uzytkownikId) :
                new ObjectParameter("uzytkownikId", typeof(string));
    
            var imieParameter = imie != null ?
                new ObjectParameter("imie", imie) :
                new ObjectParameter("imie", typeof(string));
    
            var nazwiskoParameter = nazwisko != null ?
                new ObjectParameter("nazwisko", nazwisko) :
                new ObjectParameter("nazwisko", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var krajParameter = kraj != null ?
                new ObjectParameter("kraj", kraj) :
                new ObjectParameter("kraj", typeof(string));
    
            var dataUrodzeniaParameter = dataUrodzenia.HasValue ?
                new ObjectParameter("dataUrodzenia", dataUrodzenia) :
                new ObjectParameter("dataUrodzenia", typeof(System.DateTime));
    
            var miejscowoscParameter = miejscowosc != null ?
                new ObjectParameter("miejscowosc", miejscowosc) :
                new ObjectParameter("miejscowosc", typeof(string));
    
            var rodzajMiejscowosciParameter = rodzajMiejscowosci != null ?
                new ObjectParameter("rodzajMiejscowosci", rodzajMiejscowosci) :
                new ObjectParameter("rodzajMiejscowosci", typeof(string));
    
            var plecParameter = plec != null ?
                new ObjectParameter("plec", plec) :
                new ObjectParameter("plec", typeof(string));
    
            var pozostalaIloscParameter = pozostalaIlosc.HasValue ?
                new ObjectParameter("PozostalaIlosc", pozostalaIlosc) :
                new ObjectParameter("PozostalaIlosc", typeof(int));
    
            var dataEdycjiParameter = dataEdycji != null ?
                new ObjectParameter("dataEdycji", dataEdycji) :
                new ObjectParameter("dataEdycji", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("edycja_uzytkownik", uzytkownikIdParameter, imieParameter, nazwiskoParameter, emailParameter, krajParameter, dataUrodzeniaParameter, miejscowoscParameter, rodzajMiejscowosciParameter, plecParameter, pozostalaIloscParameter, dataEdycjiParameter);
        }
    
        public virtual int liczba_albumow_artysty(string pseudonim)
        {
            var pseudonimParameter = pseudonim != null ?
                new ObjectParameter("pseudonim", pseudonim) :
                new ObjectParameter("pseudonim", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("liczba_albumow_artysty", pseudonimParameter);
        }
    
        public virtual int liczba_dni_odtwarzania_utworu(Nullable<int> utworId)
        {
            var utworIdParameter = utworId.HasValue ?
                new ObjectParameter("utworId", utworId) :
                new ObjectParameter("utworId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("liczba_dni_odtwarzania_utworu", utworIdParameter);
        }
    
        public virtual int liczba_odsluchan(string id, Nullable<int> days)
        {
            var idParameter = id != null ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(string));
    
            var daysParameter = days.HasValue ?
                new ObjectParameter("days", days) :
                new ObjectParameter("days", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("liczba_odsluchan", idParameter, daysParameter);
        }
    
        public virtual int liczba_utworow_album(string nazwa, string pseudonim)
        {
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(string));
    
            var pseudonimParameter = pseudonim != null ?
                new ObjectParameter("pseudonim", pseudonim) :
                new ObjectParameter("pseudonim", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("liczba_utworow_album", nazwaParameter, pseudonimParameter);
        }
    
        public virtual int liczba_utworow_artysty(string pseudonim)
        {
            var pseudonimParameter = pseudonim != null ?
                new ObjectParameter("pseudonim", pseudonim) :
                new ObjectParameter("pseudonim", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("liczba_utworow_artysty", pseudonimParameter);
        }
    
        public virtual int liczba_utworow_danego_gatunku(string nazwa)
        {
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("liczba_utworow_danego_gatunku", nazwaParameter);
        }
    
        public virtual int liczba_utworow_odtowrzonych_przez_uzytkownika(string imie)
        {
            var imieParameter = imie != null ?
                new ObjectParameter("imie", imie) :
                new ObjectParameter("imie", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("liczba_utworow_odtowrzonych_przez_uzytkownika", imieParameter);
        }
    
        public virtual int liczba_uzytkownikow_ktorzy_odtworzyli_utwor(string nazwa, string pseudonim)
        {
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(string));
    
            var pseudonimParameter = pseudonim != null ?
                new ObjectParameter("pseudonim", pseudonim) :
                new ObjectParameter("pseudonim", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("liczba_uzytkownikow_ktorzy_odtworzyli_utwor", nazwaParameter, pseudonimParameter);
        }
    
        public virtual ObjectResult<najpopularniejsze_w_kraju_Result> najpopularniejsze_w_kraju()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<najpopularniejsze_w_kraju_Result>("najpopularniejsze_w_kraju");
        }
    
        public virtual ObjectResult<napopularniejsi_artysci_Result> napopularniejsi_artysci(Nullable<int> liczbaDni)
        {
            var liczbaDniParameter = liczbaDni.HasValue ?
                new ObjectParameter("liczbaDni", liczbaDni) :
                new ObjectParameter("liczbaDni", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<napopularniejsi_artysci_Result>("napopularniejsi_artysci", liczbaDniParameter);
        }
    
        public virtual ObjectResult<planyIUzytkownik_Result> planyIUzytkownik(string idUzytkownika)
        {
            var idUzytkownikaParameter = idUzytkownika != null ?
                new ObjectParameter("idUzytkownika", idUzytkownika) :
                new ObjectParameter("idUzytkownika", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<planyIUzytkownik_Result>("planyIUzytkownik", idUzytkownikaParameter);
        }
    
        public virtual int srednia_odtworzen()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("srednia_odtworzen");
        }
    
        public virtual int usun_album(Nullable<int> nazwa)
        {
            var nazwaParameter = nazwa.HasValue ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usun_album", nazwaParameter);
        }
    
        public virtual int usun_artyste(Nullable<int> nazwa)
        {
            var nazwaParameter = nazwa.HasValue ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usun_artyste", nazwaParameter);
        }
    
        public virtual int usun_utwor(Nullable<int> nazwa)
        {
            var nazwaParameter = nazwa.HasValue ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usun_utwor", nazwaParameter);
        }
    
        public virtual int usun_uzytkownika(string nazwa)
        {
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("nazwa", nazwa) :
                new ObjectParameter("nazwa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usun_uzytkownika", nazwaParameter);
        }
    
        public virtual ObjectResult<utwory_z_albumu_Result> utwory_z_albumu(Nullable<int> idAlbumu)
        {
            var idAlbumuParameter = idAlbumu.HasValue ?
                new ObjectParameter("idAlbumu", idAlbumu) :
                new ObjectParameter("idAlbumu", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<utwory_z_albumu_Result>("utwory_z_albumu", idAlbumuParameter);
        }
    
        public virtual ObjectResult<wiecej_niz_srednia_Result> wiecej_niz_srednia()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<wiecej_niz_srednia_Result>("wiecej_niz_srednia");
        }
    }
}
