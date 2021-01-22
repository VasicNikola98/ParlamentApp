using ParlamentApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParlamentApp.ViewModel
{
    public class PoslanickaGrupaViewModel
    {
        public List<Poslanik> Poslanici { get; set; }
    }

    public class PoslanickaGrupaViewModel2
    {
        public List<Poslanik> Poslanici { get; set; }
        public List<Poslanik> Poslanici2 { get; set; }
        public string GrupaId { get; set; }
    }

    public class PoslanickaGrupaListingViewModel
    {
        public List<PoslanickaGrupa> Grupe { get; set; }
        public Poslanik Predsednik { get; set; }
    }

    public class PoslanickaGrupaEditViewModel
    {
        public PoslanickaGrupa Grupa { get; set; }
       
    }
}