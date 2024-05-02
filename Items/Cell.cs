using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_life.Items
{
    internal class Cell
    {
        private bool state;
        private Coordonnee coordonnees;

        public bool State
        {
            get 
            { 
                return state; 
            }
            set
            {
                state = value;
            }
        }

        public Cell(Coordonnee coordonnees)
        {
            this.state = false;
            this.coordonnees = coordonnees;
        }
    }
}
