using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterApplication.Models
{
    public class AllModel
    {
        public IEnumerable<Aktywnosc> allAktywnosc { get; set; }
        public IEnumerable<CostWater> allCostWater { get; set; }
        public IEnumerable<Ilosc> allIlosc { get; set; }
    }
}
