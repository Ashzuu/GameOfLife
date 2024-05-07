using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        /// Permet de convertir des coordonnées en indices dans le tableau
        /// C'est une méthode vraiment peu esthétique, mais it's working !
        /// </summary>
        /// <param name="i">Coordonnée en x du rectangle à retrouver</param>
        /// <param name="j">Coordonnée en y du rectangle à retrouver</param>
        /// <returns>L'indice à chercher dans le tableau correspondant</returns>
        private int ConvertToIndice(int i, int j)
        {
            int res = 0;
            if (i == 0)
            {
                res = j;
            }

            else if (j == 0)
            {
                res = i*this.limite;
            }
            else
            {
                res = i * j;
            }

            return res;
        }

        /// <summary>
        /// Permet d'obtenir le nombre de voisins vivant pour une case donnée.
        /// </summary>
        /// <returns></returns>
        private int CountVoisins(Rectangle rectangle)
        {
            int res = 0;
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
            for(int i = 0;  i < this.rectangles.GetLength(0); i++)
            {
                for(int j = 0; j < this.rectangles.GetLength(1); j++)
                {
                    
                    if (this.rectangles[i, j].Fill == Brushes.White)
                    {
                        if (CountVoisins(i, j) == 3) // Bon c'est là on ça peut devenir compliqué...
                        {
                            cellsNextStep[ConvertToIndice(i, j)] = true;
                        }
                        else
                        {
                            cellsNextStep[ConvertToIndice(i, j)] = false;
                        }
                    }
                    else
                    {
                        if (CountVoisins(i, j) < 2 || CountVoisins(i, j) > 3)
                        {
                            cellsNextStep[ConvertToIndice(i, j)] = false;
                        }
                        else
                        {
                            cellsNextStep[ConvertToIndice(i, j)] = true;
                        }
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
