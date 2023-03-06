using System;
using System.Collections.Generic;
using System.Text;

namespace ConnexioBD
{
    public class Level
    {
        String nom;
        String descripcio;
        int hores;
        int minuts;
        int segons;
        bool actiu;
        

        public Level(string nom, string descripcio, string image, int hores, int minuts, int segons, bool actiu)
        {
            this.Nom = nom;
            this.Descripcio = descripcio;
            this.Hores = hores;
            this.Minuts = minuts;
            this.Segons = segons;
            this.Actiu = actiu;
        }

        public string Nom { get => nom; set => nom = value; }
        public string Descripcio { get => descripcio; set => descripcio = value; }
        public int Hores { get => hores; set => hores = value; }
        public int Minuts { get => minuts; set => minuts = value; }
        public int Segons { get => segons; set => segons = value; }
        public bool Actiu { get => actiu; set => actiu = value; }
    }
}
