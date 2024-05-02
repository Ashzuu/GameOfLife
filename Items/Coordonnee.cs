using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_life.Items
{
    internal class Coordonnee
    {
        private int x;
        private int y;

        public Coordonnee(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coordonnee()
        {
            this.x = 0;
            this.y = 0;
        }

        public List<Coordonnee> GetAllVoisin()
        {
            List<Coordonnee> voisins = new List<Coordonnee>();
            for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    if(x != 0 && y != 0)
                    {
                        voisins.Add(new Coordonnee(this.x + x, this.y + y));
                    }
                    
                }
            }
            return voisins;
        }
    }
}
