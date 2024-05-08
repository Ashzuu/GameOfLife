using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_of_life
{
    public class Jeu
    {
        
        #region Attributes
        
        private Dictionary<Rectangle,bool> cells;
        private Rectangle[,] rectangles;
        private int limite;
        private bool isAutomatic;
        
        #endregion

        #region Properties

        public bool IsAutomatic
        {
            get
            {
                return this.isAutomatic;
            }

            set
            {
                this.isAutomatic = value;
            }
        }
        
        #endregion
        
        #region Constructor

        /// <summary>
        /// Constructeur de la classe jeu
        /// </summary>
        /// <param name="rectangle">Les rectangles pré-initalisés dans la classe Game.xaml.cs</param>
        public Jeu(Rectangle[,] rectangle,int limite)
        {
            this.rectangles = rectangle;
            this.limite = limite;
            this.isAutomatic = false;
            Initialisation();
        }
        
        #endregion

        #region Methods
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
        /// <returns>Le nombre de voisins d'une cellule donnée</returns>
        private int CountVoisins(Rectangle rectangle)
        {
            int res = 0;

            int i = 0;
            int j = 0;
            foreach (Rectangle rect in this.cells.Keys)                    // Ici je dois récupérer l'indice du rectangle associée...
            {
                if (rectangle != rect) i += 1;
                else break;
            }

            j = i % this.limite;                                           // Etant donné que c'est en 2 dimensions, je divise pour que ça corresponde
            i = i / this.limite;
            
            if (i > 0 && j > 0 && j < this.limite-1 && i < this.limite-1 ) // Ici c'est vraiment moche... Sincère excuses à mes profs d'IUT !
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
        /// Mise à jour l'écran 
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
                int nbVoisin = CountVoisins(rectangle);
                if (rectangle.Fill == Brushes.White)
                {
                    if (nbVoisin == 3) // Bon c'est là on ça peut devenir compliqué...
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
                    if (nbVoisin < 2 || nbVoisin > 3)
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
        #endregion
    }
}
