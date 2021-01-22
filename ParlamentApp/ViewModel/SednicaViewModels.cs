using ParlamentApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParlamentApp.ViewModel
{
    public class SednicaViewListingModel
    {
        public List<Sednica> Sednice { get; set; }
      
    }

    public class SednicaViewListingModel2
    {
        public List<Sednica> Sednice { get; set; }
        public List<Sednica> Sednice2 { get; set; }
        public string PoslanikId { get; set; }

    }
}