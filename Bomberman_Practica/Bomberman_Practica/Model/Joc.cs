using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman_Practica.Model
{
    internal class Joc
    {
        public List<Level> nivells;

        public Joc(List<Level> nivells)
        {
            Nivells = nivells;
        }

        public List<Level> Nivells { get => nivells; set => nivells = value; }
    }
}
