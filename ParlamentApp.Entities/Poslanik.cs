using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlamentApp.Entities
{
    public class Poslanik
    {
        public string Id { get; set; }
        public string LicnoIme { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public string AdresaStanovanja { get; set; }
        public string MestoStanovanja { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string MestoRodjenja { get; set; }
        public string Email { get; set; }
        public string IzbornaLista { get; set; }
    }
}
