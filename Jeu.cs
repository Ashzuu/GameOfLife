using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_of_life
{
    public class Jeu
    {
        private Dictionary<Rectangle,bool> cells;
        private Rectangle[,] rectangles;
        private int limite;

        /// <summary>
        /// Constructeur de la classe jeu
        /// </summary>
        /// <param name="rectangle">Les rectangles pré-initalisés dans la classe Game.xaml.cs</param>
        public Jeu(Rectangle[,] rectangle,int limite)
        {
            this.rectangles = rectangle;
            this.limite = limite;
            Initialisation();
        }

        /// <summary>
        /// Initialisation de la grid.
        /// </summary>
        private void Initialisation()
        {
            this.cells = new Dictionary<Rectangle, bool>();
            foreach (Rectangle rectangle in this.rectangles)
            {
                if (rectangle.Fill == Brushes.White)
                {
                    this.cells[rectangle] = false;
                }
                else
                {
                    this.cells[rectangle] = true;
                }
            }           
        }

        /// <summary>
        /// Permet d'obtenir le nombre de voisins vivant pour une case donnée.
        /// </summary>
        /// <returns></returns>
        private int CountVoisins(Rectangle rectangle)
        {
            int res = 0;

            int i = 0;
            int j = 0;
            foreach (Rectangle rect in this.cells.Keys)
            {
                if (rectangle != rect) i += 1;
                else break;
            }

            j = i % this.limite;
            i = i / this.limite;
            
            if (i > 0 && j > 0 && j < this.limite-1 && i < this.limite-1 ) 
            {
                if (this.rectangles[i-1, j-1].Fill == Brushes.Black) res += 1;
                if (this.rectangles[i-1, j].Fill == Brushes.Black) res += 1;
                if (this.rectangles[i-1, j+1].Fill == Brushes.Black) res += 1;
                if (this.rectangles[i, j-1].Fill == Brushes.Black) res += 1;
                if (this.rectangles[i, j+1].Fill == Brushes.Black) res += 1;
                if (this.rectangles[i+1, j-1].Fill == Brushes.Black) res += 1;
                if (this.rectangles[i+1, j].Fill == Brushes.Black) res += 1;
                if (this.rectangles[i+1, j+1].Fill == Brushes.Black) res += 1;
            }
            return res;
        }

        /// <summary>
        /// Mets à jour l'écran 
        /// </summary>
        private void updateScreen()
        {
            foreach (KeyValuePair<Rectangle,bool> cell in this.cells)
            {
                if (cell.Value)
                {
                    cell.Key.Fill = Brushes.Black;
                }
                else
                {
                    cell.Key.Fill = Brushes.White;
                }
            }
        }
        
        /// <summary>
        /// Permet de lancer le jeu !
        /// </summary>
        public void Jouer()
        {
            Dictionary<Rectangle,bool> cellsNextStep = new Dictionary<Rectangle,bool>(this.cells);
            foreach (Rectangle rectangle in this.rectangles)
            {
                    if (rectangle.Fill == Brushes.White)
                    {
                        if (CountVoisins(rectangle) == 3) // Bon c'est là on ça peut devenir compliqué...
                        {
                            cellsNextStep[rectangle] = true;
                        }
                        else
                        {
                            cellsNextStep[rectangle] = false;
                        }
                    }
                    else
                    {
                        if (CountVoisins(rectangle) < 2 || CountVoisins(rectangle) > 3)
                        {
                            cellsNextStep[rectangle] = false;
                        }
                        else
                        {
                            cellsNextStep[rectangle] = true;
                        }
                    }
            }
            if (cellsNextStep.Count != this.cells.Count)
                throw new Exception("Erreur lors du calcul de la nouvelle liste de cellules !");
            this.cells = cellsNextStep;
            updateScreen();
        }
    }
}
