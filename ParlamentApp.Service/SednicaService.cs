using Neo4j.Driver;
using ParlamentApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlamentApp.Service
{
    public class SednicaService
    {
        #region Singleton
        public static SednicaService Instance
        {
            get
            {
                if (instance == null) instance = new SednicaService();

                return instance;
            }
        }

        private static SednicaService instance { get; set; }

        private SednicaService()
        {

        }
        #endregion

        public List<Poslanik> VratiSvePrisutne(string sednicaId)
        {
            var session = SessionManager.GetSession();
            List<Poslanik> poslanici = new List<Poslanik>();
            var test = session.WriteTransaction(t =>
             {
                 var rezultat = t.Run(@"match (a:Poslanik)-[:Prisutnost]->(b:Sednica) WHERE id(b) = "+ sednicaId + " return a").ToList();

                 return rezultat;
             });

            foreach (var t in test)
            {
                var node = t["a"].As<INode>();
                Poslanik poslanik = new Poslanik();

                // poslanik.Id = node["id"].As<string>();
                poslanik.Id = node.Id.ToString();
                poslanik.AdresaStanovanja = node["AdresaStanovanja"].As<string>();
                poslanik.DatumRodjenja = node["DatumRodjenja"].As<DateTime>();
                poslanik.Email = node["Email"].As<string>();
                poslanik.IzbornaLista = node["IzbornaLista"].As<string>();
                poslanik.Jmbg = node["Jmbg"].As<string>();
                poslanik.LicnoIme = node["LicnoIme"].As<string>();
                poslanik.MestoRodjenja = node["MestoRodjenja"].As<string>();
                poslanik.MestoStanovanja = node["MestoStanovanja"].As<string>();
                poslanik.Prezime = node["Prezime"].As<string>();



                poslanici.Add(poslanik);
            }

            return poslanici;
        }


        public void DodajSednicu(Sednica sednica)
        {
            var session = SessionManager.GetSession();

            session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"CREATE (n:Sednica {DatumPocetka: '" + sednica.DatumPocetka + "' , DatumZavrsetka: '" + sednica.DatumZavrsetka + "', BrojZasedanja: '" + sednica.BrojZasedanja + "', BrojSaziva: ' " + sednica.BrojSaziva + "'}) return n");

                return rezultat;
            });
        }

        public void NijePrisustvovao(string PoslanikId, string SednicaId)
        {
            var session = SessionManager.GetSession();



                session.WriteTransaction(t =>
                {
                    var rezultat = t.Run(@"MATCH (a:Poslanik)-[r:Prisutnost]->(b:Sednica) WHERE id(a) = " + PoslanikId + " AND id(b) = " + SednicaId + " delete r");
                    return rezultat;
                });


        }

        public void PrisustvovaoJeSednici(string PoslanikId, string SednicaId)
        {
            var session = SessionManager.GetSession();

                var cvor =session.WriteTransaction(t =>
                {
                    var rezultat = t.Run(@"MATCH  (n:Poslanik), (b:Sednica) WHERE id(n)= "+int.Parse(PoslanikId)+" AND id(b)="+int.Parse(SednicaId)+ " RETURN EXISTS( (n)-[:Prisutnost]->(b) )");
                    return rezultat.Single()[0].As<bool>();
                });



            if (!cvor)
            {
                session.WriteTransaction(t =>
                {
                    var rezultat = t.Run(@"MATCH (a:Poslanik),(b:Sednica) WHERE id(a) = " + PoslanikId + " AND id(b) = " + SednicaId + " CREATE (a)-[r:Prisutnost]->(b)  RETURN r");
                    return rezultat;
                });

            }


            
        }

        public List<Sednica> SveSednice()
        {

            List<Sednica> sednice = new List<Sednica>();
            var session = SessionManager.GetSession();

            var test = session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"MATCH (n:Sednica) RETURN n").ToList();

                return rezultat;
            });

            foreach (var t in test)
            {
                var node = t["n"].As<INode>();
                Sednica sednica = new Sednica();

                sednica.Id = node.Id.ToString();
                sednica.BrojZasedanja = node["BrojZasedanja"].As<string>();
                sednica.BrojSaziva = node["BrojSaziva"].As<string>();
                sednica.DatumPocetka = node["DatumPocetka"].As<DateTime>();
                sednica.DatumZavrsetka = node["DatumZavrsetka"].As<DateTime>();

                sednice.Add(sednica);
            }

            return sednice;
        }


        public List<Sednica> SveSednice(string id)
        {

            List<Sednica> sednice = new List<Sednica>();
            var session = SessionManager.GetSession();

            var test = session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"match (a:Poslanik)-[:Prisutnost]->(b:Sednica) WHERE id(a) = " + id + " return b").ToList();

                return rezultat;
            });

            foreach (var t in test)
            {
                var node = t["b"].As<INode>();
                Sednica sednica = new Sednica();

                sednica.Id = node.Id.ToString();
                sednica.BrojZasedanja = node["BrojZasedanja"].As<string>();
                sednica.BrojSaziva = node["BrojSaziva"].As<string>();
                sednica.DatumPocetka = node["DatumPocetka"].As<DateTime>();
                sednica.DatumZavrsetka = node["DatumZavrsetka"].As<DateTime>();

                sednice.Add(sednica);
            }

            return sednice;
        }

        public void IzbrisiSednicu(string IdSednice)
        {
            var session = SessionManager.GetSession();

            var test = session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"match (n:Sednica) where id(n) = " + IdSednice + "detach delete n");

                return rezultat;
            });
        }
    }
}
