using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient.Cypher;
using Neo4jClient;
using Neo4j.Driver;
using ParlamentApp.Entities;

namespace ParlamentApp.Service
{
    public class PoslanikService
    {

        #region Singleton
        public static PoslanikService Instance
        {
            get
            {
                if (instance == null) instance = new PoslanikService();

                return instance;
            }
        }

        private static PoslanikService instance { get; set; }

        private PoslanikService()
        {

        }
        #endregion


        public List<Poslanik> SviPoslanici()
        {
          
            List<Poslanik> poslanici = new List<Poslanik>();
            var session = SessionManager.GetSession();

            var test = session.WriteTransaction(t =>
             {
                 var rezultat = t.Run(@"MATCH (n:Poslanik) RETURN n").ToList();

                 return rezultat;
             });
   
            foreach(var t in test)
            {
                var node = t["n"].As<INode>();
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

        public void DodajPoslanika(Poslanik poslanik)
        {
           var session = SessionManager.GetSession();

           session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"CREATE (n:Poslanik {LicnoIme: '" + poslanik.LicnoIme + "' , Prezime: '" + poslanik.Prezime + "' , Jmbg: '" + poslanik.Jmbg + "' , AdresaStanovanja: '" + poslanik.AdresaStanovanja + "', MestoStanovanja: '" + poslanik.MestoStanovanja + "' ,DatumRodjenja: '" + poslanik.DatumRodjenja + "' ,MestoRodjenja: '" + poslanik.MestoRodjenja + "' ,Email: '" + poslanik.Email + "', IzbornaLista: '" + poslanik.IzbornaLista + "' }) return n");

                return rezultat;
            });
        }

      

    }
}
