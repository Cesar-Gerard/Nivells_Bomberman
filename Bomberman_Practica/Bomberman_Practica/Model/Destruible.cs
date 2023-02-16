using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    public class Destruible
    {
        int id;
        String url;
        bool destruit;

        public Destruible(int id, string url, bool destruit)
        {
            Id = id;
            Url = url;
            Destruit = destruit;
        }

        public int Id { get => id; set => id = value; }
        public string Url { get => url; set => url = value; }
        public bool Destruit { get => destruit; set => destruit = value; }
    }
}
