using System;
using System.Collections.Generic;

#nullable disable

namespace NyDataBasInlämningsUppgift.Models.Entities
{
    public partial class TempDatum
    {
        public DateTime Datum { get; set; }
        public string Plats { get; set; }
        public double Temp { get; set; }
        public int Luftfuktighet { get; set; }
    }
}
