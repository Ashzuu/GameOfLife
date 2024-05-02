using Game_of_life.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_of_life
{
    internal class Jeu
    {

        private List<Cell> cells;

        public Jeu(Rectangle[,] rectangle)
        {
            this.cells = new List<Cell>();
            for(int i = 0;  i < rectangle.GetLength(0); i++)
            {
                for(int j = 0; j < rectangle.GetLength(1); j++)
                {
                    Cell cellule = new Cell(new Coordonnee(i, j));
                    if(rectangle[i,j].Fill == Brushes.Black)
                    {
                        cellule.State = true;
                    }
                    this.cells.Add(cellule);
                }
            }            
        }

        public void Jouer()
        {
            throw new NotImplementedException();
        }
    }
}
