using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlamentApp.Entities
{
    public class Sednica
    {
        public string Id { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public string BrojSaziva { get; set; }
        public string BrojZasedanja { get; set; }
    }
}
