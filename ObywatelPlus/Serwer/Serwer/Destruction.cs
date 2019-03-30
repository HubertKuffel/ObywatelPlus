using System;
using System.Collections.Generic;
using System.Text;

namespace Serwer
{
    class Destruction
    {
        public string Picture { get; set; }
        public string GPSCoordinates { get; set; }
        public string Description { get; set; }
        public Destruction(string picture, string gPSCoordinates, string description)
        {
            picture = Picture;
            gPSCoordinates = GPSCoordinates;
            description = Description;
        }
    }
}
