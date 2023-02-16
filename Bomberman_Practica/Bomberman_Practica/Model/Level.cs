using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    public abstract class Level
    {
        String nom;
        String descripcio;
        String image;
        int temps;
        bool actiu;

        protected Level(string nom, string descripcio, string image, int temps, bool actiu)
        {
            Nom = nom;
            Descripcio = descripcio;
            Image = image;
            Temps = temps;
            Actiu = actiu;
        }

        public string Nom { get => nom; set => nom = value; }
        public string Descripcio { get => descripcio; set => descripcio = value; }
        public string Image { get => image; set => image = value; }
        public int Temps { get => temps; set => temps = value; }
        public bool Actiu { get => actiu; set => actiu = value; }
    }
}
