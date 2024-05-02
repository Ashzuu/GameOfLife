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
        #region Attributes
        
        private bool state; // Etat d'une cellule
        private Coordonnee coordonnees; // Coordonnée d'une cellule

        #endregion

        #region Properties

        /// <summary>
        /// Retourne ou change l'état de la cellule
        /// </summary>
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

        /// <summary>
        /// Retourne les coordonnées de la cellule
        /// </summary>
        public Coordonnee Coordonnee
        {
            get
            {
                return coordonnees;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructeur de la classe Cell
        /// </summary>
        /// <param name="coordonnees">Coordonnée de la cellule</param>
        public Cell(Coordonnee coordonnees)
        {
            this.state = false;
            this.coordonnees = coordonnees;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Récupère la liste des cellules vivantes
        /// </summary>
        /// <param name="cellules">Liste des cellules</param>
        /// <returns>Liste des voisins vivants</returns>
        private List<Cell> GetCellsLivedVoisins(List<Cell> cellules)
        {
            List<Coordonnee> coordonneesVoisines = new List<Coordonnee>(this.coordonnees.GetAllVoisin());
            List<Cell> voisins = new List<Cell>();
            foreach (Coordonnee co in coordonneesVoisines)
            {
                if (co.CoordonneeX > 0 && co.CoordonneeX < Game.Limit && co.CoordonneY > 0 && co.CoordonneY < Game.Limit)
                {
                    Cell cell = SearchByCoordonnee(co, cellules);
                    if (cell.State)
                    {
                        voisins.Add(cell);
                    }
                }
            }
            return voisins;
        }

        /// <summary>
        /// Rechercher à partir de la liste des cellules et d'une coordonnée une cellule
        /// </summary>
        /// <param name="coordonnee">Coordonnée de la cellule cible</param>
        /// <param name="cellules">Liste des cellules</param>
        /// <returns>Cellule Cible</returns>
        /// <exception cref="Exception">Lorsque la cellule n'a pas été trouvé (donc les coordonnées incorrectes)</exception>
        private Cell SearchByCoordonnee(Coordonnee coordonnee, List<Cell> cellules)
        {
            Cell res = null;
            foreach(Cell cell in cellules)
            {
                if (cell.Coordonnee == coordonnee)
                {
                    res = cell;
                    break;
                }
            }
            if(res != null)
            {
                return res;
            }
            else
            {
                throw new Exception("Coordonnées invalides !");
            }
        }

        #endregion
    }
}
