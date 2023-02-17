using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    public class Casella
    {
        //Constructor de la clase casella per la llista de tipus de caselles 

        private string nom;
        private string img;

        public Casella(string nom, string img)
        {
            Nom = nom;
            Img = img;
        }

        public string Nom { get => nom; set => nom = value; }
        public string Img { get => img; set => img = value; }

        public static List<Casella> llistacasellas()
        {
            List<Casella> resultat = new List<Casella>();

            resultat.Add(new Casella("Fons","/Assets/fons_grid.png"));
            resultat.Add(new Casella("Indestructible", "/Assets/indestructible.png"));
            resultat.Add(new Casella("Destructible", "/Assets/soft.png"));
            resultat.Add(new Casella("Enemic", "/Assets/enemy.png"));

            return resultat;
        }
    }
}
