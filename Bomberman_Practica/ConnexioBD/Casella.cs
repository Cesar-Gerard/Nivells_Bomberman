using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    //Constructor de la clase casella per la llista de tipus de caselles 
    public class Casella
    {
        
        //Atributs de la classe
        private string nom;
        private string img;
        private int id;
        private int cX;
        private int cY;


        #region Constructors
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

        #endregion

        #region Getters i Setters
        public string Nom { get => nom; set => nom = value; }
        public string Img { get => img; set => img = value; }
        public int Id { get => id; set => id = value; }
        public int CX { get => cX; set => cX = value; }
        public int CY { get => cY; set => cY = value; }

        #endregion



        //Métodes de la classe


        /// <summary>
        /// Retorna un llistat del tipus de caselles existents
        /// </summary>
        /// <returns></returns>
        public static List<Casella> llistacasellas()
        {
            List<Casella> resultat = new List<Casella>();

            resultat.Add(new Casella("Fons","/Assets/fons_grid.png",1));
            resultat.Add(new Casella("Indestructible", "/Assets/indestructible.png",2));
            resultat.Add(new Casella("Destructible", "/Assets/soft.png",3));
            resultat.Add(new Casella("Enemic", "/Assets/enemy.png",4));
            resultat.Add(new Casella("Inici", "/Assets/start_point.png",5));
            resultat.Add(new Casella("Final", "/Assets/finsih_point.png",6));

            return resultat;
        }


        /// <summary>
        /// Retorna un string depenent del valor del id de la casella
        /// </summary>
        /// <param name="valor"></param>
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

                case 5:
                    resultat = "/Assets/start_point.png";
                    this.img = resultat;
                    break; 

                case 6:
                    resultat = "/Assets/finsih_point.png";
                    this.img = resultat;
                    break;


            }



        }

    }
}
