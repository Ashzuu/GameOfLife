using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

// Attention : Le code est d'une laideur inouïe...
// Promis un jour je l'améliorerai !
// Enfin j'essaierai...

namespace Game_of_life
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {

        #region Attributes

        private Rectangle[,] rectangles;        // Stocke les différents rectangles représentants les cases
        private int limit;                      // Définit la limite de l'indice du tableau.
        private bool randomPattern;             // Définit si l'on doit générer un pattern aléatoire ou non.

        #endregion

        #region Properties

        public static int Limit
        {
            get
            {
                return 0;
            }
        }

        #endregion

        /// <summary>
        /// Constructeur de la classe "Game" (Initialise la page)
        /// </summary>
        /// <param name="valueSlider">Valeur choisie au préalable dans le menu principal</param>
        public Game(int valueSlider)
        {
            InitializeComponent();
            Random nbRandom = new Random();
            this.limit = valueSlider;
            this.rectangles = new Rectangle[valueSlider, valueSlider];
            for (int i = 0; i < valueSlider; i++)
            {
                this.Grille.ColumnDefinitions.Add(new ColumnDefinition());
                this.Grille.RowDefinitions.Add(new RowDefinition());
            }

            for (int x = 0; x < valueSlider; x++)           // Parcours de la grille permettant d'y ajouter tous les éléments
            {
                for (int y = 0; y < valueSlider; y++)
                {
                    Rectangle rectangle = CreateRectangle();
                    if (this.randomPattern)                 // Must be patch !
                    {
                        int nb = nbRandom.Next(1, 3);
                        if (nb == 1)
                        {
                            rectangle.Fill = Brushes.Black;
                        }
                    }
                    SetInGrid(Grille, rectangle, x, y);
                    this.rectangles[x, y] = rectangle;
                }
            }

        }

        /// <summary>
        /// Méthode créant un rectangle spécifique au jeu
        /// </summary>
        /// <returns>Le nouveau Rectangle</returns>
        private Rectangle CreateRectangle()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.HorizontalAlignment = HorizontalAlignment.Stretch;
            rectangle.VerticalAlignment = VerticalAlignment.Stretch;
            rectangle.Fill = Brushes.Transparent;
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 0.1;
            rectangle.MouseLeftButtonDown += MyRectangle_MouseLeftButtonDown;

            return rectangle;
        }
        
        /// <summary>
        /// Evenement permettant de changer la couleur d'une rectangle lors d'un clique gauche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle clickedObj = sender as Rectangle;

            if (clickedObj.Fill == Brushes.White) clickedObj.Fill = Brushes.Black;
            else clickedObj.Fill = Brushes.White;

        }

        /// <summary>
        /// Permet d'ajouter un élément rectangle dans une grille donnée en paramètre
        /// </summary>
        /// <param name="grille">La grille dans laquelle ajouter</param>
        /// <param name="rectangle">Le rectangle à ajouter</param>
        /// <param name="x">Coordonnée x</param>
        /// <param name="y">Coordonnée y</param>
        private void SetInGrid(Grid grille, Rectangle rectangle, int x, int y)
        {
            Grid.SetColumn(rectangle, x);
            Grid.SetRow(rectangle, y);
            Grille.Children.Add(rectangle);
        }

        /// <summary>
        /// Permet de définir si le random pattern est activé ou non.
        /// </summary>
        /// <param name="value"></param>
        public void DefineCheckBox(bool value)
        {
            this.randomPattern = value;
        }

        /// <summary>
        /// Permet d'appeler lors de l'appui de la touche E !
        /// </summary>
        /// <param name="sender">Objet qui appelle la fonction</param>
        /// <param name="e">La clé qui a permis d'appeler la fonction</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.E)
            {
                Jeu jeu = new Jeu(this.rectangles,this.limit);
                jeu.Jouer();
            }
        }
    }
}
