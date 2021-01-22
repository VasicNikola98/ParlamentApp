using Neo4j.Driver;
using ParlamentApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlamentApp.Service
{
    public class PoslanickaGrupaService
    {
        #region Singleton
        public static PoslanickaGrupaService Instance
        {
            get
            {
                if (instance == null) instance = new PoslanickaGrupaService();

                return instance;
            }
        }

        private static PoslanickaGrupaService instance { get; set; }

        private PoslanickaGrupaService()
        {

        }
        #endregion

        public void DodajGrupu(PoslanickaGrupa grupa)
        {
            var session = SessionManager.GetSession();

            var pom=session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"CREATE (n:PoslanickaGrupa {NazivGrupe: '" + grupa.NazivGrupe + "' , IdPredsednika: '" + grupa.IdPredsednika + "' }) return n");

                return rezultat.Single()[0].As<INode>();
            });

            //var node = pom["n"].As<INode>();

            var idGrupe = pom.Id.ToString();

            session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"MATCH (a:Poslanik),(b:PoslanickaGrupa) WHERE id(a) = " + grupa.IdPredsednika + " AND id(b) = " + idGrupe + " CREATE (a)-[r:Je_Predsednik]->(b)  RETURN r");
                return rezultat;
            });

            session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"MATCH (a:Poslanik),(b:PoslanickaGrupa) WHERE id(a) = " + grupa.IdPredsednika + " AND id(b) = " + idGrupe + " CREATE (a)<-[r:Je_Predsednik]-(b)  RETURN r");
                return rezultat;
            });
        }

        public void DodajClanaUGrupu(string IdGrupe, string IdPoslanika)
        {
            var session = SessionManager.GetSession();

            session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"MATCH (a:Poslanik),(b:PoslanickaGrupa) WHERE id(a) = " + int.Parse(IdPoslanika) + " AND id(b) = " + int.Parse(IdGrupe) + " CREATE (a)-[r:Je_Clan]->(b)  RETURN r");
                return rezultat;

              
            });
        }

        public List<PoslanickaGrupa> SveGrupe()
        {

            List<PoslanickaGrupa> grupe = new List<PoslanickaGrupa>();
            var session = SessionManager.GetSession();

            var test = session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"MATCH (n:PoslanickaGrupa) RETURN n").ToList();

                return rezultat;
            });

            foreach (var t in test)
            {
                var node = t["n"].As<INode>();
                PoslanickaGrupa grupa = new PoslanickaGrupa();

                grupa.Id = node.Id.ToString();
                grupa.NazivGrupe = node["NazivGrupe"].As<string>();
                grupa.IdPredsednika = node["IdPredsednika"].As<string>();
                grupe.Add(grupa);
            }

            return grupe;
        }

        public List<Poslanik> SviPoslaniciGrupe(string idGrupe)
        {

            var session = SessionManager.GetSession();
            List<Poslanik> poslanici = new List<Poslanik>();
            var test = session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"match (a:Poslanik)-[:Je_Clan]->(b:PoslanickaGrupa) WHERE id(b) = " + idGrupe + " return a").ToList();

                return rezultat;
            });

            foreach (var t in test)
            {
                var node = t["a"].As<INode>();
                Poslanik poslanik = new Poslanik();

                
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

        public void IzbaciClanaIzGrupe(string IdGrupe, string IdPoslanika)
        {
            var session = SessionManager.GetSession();

            var test = session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"match (a:Poslanik)-[r:Je_Clan]->(b:PoslanickaGrupa) WHERE id(b) = " + IdGrupe + " AND id(a) =" + IdPoslanika + " DELETE r");


                return rezultat;
            });

        }

        public PoslanickaGrupa VratiGrupu(string id)
        {

            var session = SessionManager.GetSession();

            var test = session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@"MATCH (n:PoslanickaGrupa) WHERE id(n) = " + id + " RETURN n").FirstOrDefault();

                return rezultat;
            });

           
                var node = test["n"].As<INode>();
                PoslanickaGrupa grupa = new PoslanickaGrupa();

                grupa.Id = node.Id.ToString();
                grupa.NazivGrupe = node["NazivGrupe"].As<string>();
                grupa.IdPredsednika = node["IdPredsednika"].As<string>();
 
            return grupa;
        }

        public void IzmeniNazivGrupe(string IdGrupe, string NazivGrupe)
        {
            var session = SessionManager.GetSession();

            var test = session.WriteTransaction(t =>
            {
                var rezultat = t.Run(@" MATCH(n:PoslanickaGrupa) WHERE id(n) = " + IdGrupe + " SET n.NazivGrupe = '" + NazivGrupe + "' RETURN n");

                return rezultat;
            });
           
        }
    }
}
