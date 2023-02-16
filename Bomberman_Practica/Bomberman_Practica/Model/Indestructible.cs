using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    public class Indestructible
    {
        int id;
        String url;
        bool destruible;

        public Indestructible(int id, string url, bool destruible)
        {
            Id = id;
            Url = url;
            Destruible = destruible;
        }

        public int Id { get => id; set => id = value; }
        public string Url { get => url; set => url = value; }
        public bool Destruible { get => destruible; set => destruible = value; }
    }
}
