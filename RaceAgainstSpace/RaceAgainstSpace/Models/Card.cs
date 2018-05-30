using System;
using System.Collections.Generic;
using System.Text;

namespace RaceAgainstSpace.Models
{
    public class Card
    {
        public string Text { get; set; }
        public string Subtext { get; set; }
        public string Type { get; set; }
        // TODO: enum this
        /*
        public enum type
        {
            star,
            planet,
            anomaly

        }
        */
    }
}
