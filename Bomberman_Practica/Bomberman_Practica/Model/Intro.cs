using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    public class Intro : Level
    {
        String url;

        public Intro(string nom, string descripcio, string image, int temps, string url, bool actiu) : base(nom, descripcio, image, temps, actiu)
        {
            Url = url;
        }

        public string Url { get => url; set => url = value; }
    }
}
