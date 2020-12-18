using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterApplication.Models
{
    public class Ilosc
    {
        public int Id { get; set; }
        public int Ammount { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }      
        public Aktywnosc Aktywnosc { get; set; }
    }
}
