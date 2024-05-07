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
    internal class Jeu
    {
        private List<bool> cells;
        private Rectangle[,] rectangles;
        private int limite;

        /// <summary>
        /// Constructeur de la classe jeu
        /// </summary>
        /// <param name="rectangle">Les rectangles pré-initalisés dans la classe Game.xaml.cs</param>
        public Jeu(Rectangle[,] rectangle,int limite)
        {
            this.rectangles = rectangle;
            this.cells = new List<bool>();
            this.limite = limite;
            for(int i = 0;  i < this.rectangles.GetLength(0); i++)
            {
                for(int j = 0; j < this.rectangles.GetLength(1); j++)
                {
                    if(this.rectangles[i,j].Fill == Brushes.Black)
                    {
                        this.cells.Add(true);
                    }
                    else
                    {
                        this.cells.Add(false);
                    }
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
                res = i;
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
        private int CountVoisins(int i, int j)
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
            for (int i = 0; i < this.rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < this.rectangles.GetLength(1); j++)
                {
                    if (this.cells[ConvertToIndice(i,j)])
                    {
                        this.rectangles[i, j].Fill = Brushes.Black;
                    }
                    else
                    {
                        this.rectangles[i, j].Fill = Brushes.White;
                    }
                }
            }
        }
        
        /// <summary>
        /// Permet de lancer le jeu !
        /// </summary>
        public void Jouer()
        {
            List<bool> cellsNextStep = new List<bool>(this.cells);
            for(int i = 0;  i < this.rectangles.GetLength(0); i++)
            {
                for(int j = 0; j < this.rectangles.GetLength(1); j++)
                {
                    
                    if (this.rectangles[i, j].Fill == Brushes.White)
                    {
                        if (CountVoisins(i, j) == 3)
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
