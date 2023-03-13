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
        private int id;
        private int cX;
        private int cY;

        public Casella(string nom, string img, int id)
        {
            Nom = nom;
            Img = img;
            this.Id = id;
        }

        public Casella(int id, int cX, int cY)
        {
            Nom = nom;
            Img = img;
            this.Id = id;
            CX = cX;
            CY = cY;
            
        }

        public string Nom { get => nom; set => nom = value; }
        public string Img { get => img; set => img = value; }
        public int Id { get => id; set => id = value; }
        public int CX { get => cX; set => cX = value; }
        public int CY { get => cY; set => cY = value; }

        public static List<Casella> llistacasellas()
        {
            List<Casella> resultat = new List<Casella>();

            resultat.Add(new Casella("Fons","/Assets/fons_grid.png",1));
            resultat.Add(new Casella("Indestructible", "/Assets/indestructible.png",2));
            resultat.Add(new Casella("Destructible", "/Assets/soft.png",3));
            resultat.Add(new Casella("Enemic", "/Assets/enemy.png",4));

            return resultat;
        }



        public void retornaImatgeString(int valor)
        {
            String resultat = null;

            switch (valor)
            {
                case 1:
                    resultat = "/Assets/fons_grid.png";
                    this.img = resultat;

                break;


                case 2:
                    resultat = "/Assets/indestructible.png";
                    this.img = resultat;

                    break;


                case 3:
                    resultat = "/Assets/soft.png";
                    this.img = resultat;
                    break;


                case 4:
                    resultat = "/Assets/enemy.png";
                    this.img = resultat;
                    break;


            }



        }



    }
}
