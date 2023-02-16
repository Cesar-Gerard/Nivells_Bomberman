using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    public class Enemic
    {
        int id;
        String url;
        int posicio;
        int direccio;
        bool mort;

        public Enemic(int id, string url, int posicio, int direccio, bool mort)
        {
            Id = id;
            Url = url;
            Posicio = posicio;
            Direccio = direccio;
            Mort = mort;
        }

        public int Id { get => id; set => id = value; }
        public string Url { get => url; set => url = value; }
        public int Posicio { get => posicio; set => posicio = value; }
        public int Direccio { get => direccio; set => direccio = value; }
        public bool Mort { get => mort; set => mort = value; }
    }
}
