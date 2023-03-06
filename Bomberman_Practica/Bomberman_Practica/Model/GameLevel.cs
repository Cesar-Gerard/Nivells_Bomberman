using ConnexioBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    public class GameLevel : Level
    {
        int id;
        List<Destruible> destruibles;
        List<Indestructible> indestructibles;
        List<Enemic> enemics;
        Inici inici;
        Fi final;
        String back;

        public GameLevel(string nom, string descripcio, string image, int hores, int minuts, int segons, bool actiu) : base(nom, descripcio, image, hores, minuts, segons, actiu)
        {
            Id = id;
            Destruibles = destruibles;
            Indestructibles = indestructibles;
            Enemics = enemics;
            Inici = inici;
            Final = final;
            Back = back;
        }

        public int Id { get => id; set => id = value; }
        public List<Destruible> Destruibles { get => destruibles; set => destruibles = value; }
        public List<Indestructible> Indestructibles { get => indestructibles; set => indestructibles = value; }
        public List<Enemic> Enemics { get => enemics; set => enemics = value; }
        public Inici Inici { get => inici; set => inici = value; }
        public Fi Final { get => final; set => final = value; }
        public string Back { get => back; set => back = value; }
    }
}
