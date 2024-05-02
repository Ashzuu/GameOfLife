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
        private Rectangle[,] rectangles;        // Stocke les différents rectangles représentants les cases

        /// <summary>
        /// Constructeur de la classe "Game" (Initialise la page)
        /// </summary>
        /// <param name="valueSlider">Valeur choisie au préalable dans le menu principal</param>
        public Game(int valueSlider)
        {
            InitializeComponent();
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
                    Button button = CreateButton();
                    SetInGrid(Grille, rectangle, x, y);
                    SetInGrid(Grille, button, x, y);
                    this.rectangles[x, y] = rectangle;
                }
            }

        }

        /// <summary>
        /// Méthode créant un bouton spécifique au jeu
        /// </summary>
        /// <returns></returns>
        private Button CreateButton()
        {
            Button button = new Button();
            button.Opacity = 0;
            button.Click += onButtonClick;
            button.HorizontalAlignment = HorizontalAlignment.Stretch;
            button.VerticalAlignment = VerticalAlignment.Stretch;
            
            return button;
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

            return rectangle;
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
        /// Permet d'ajouter un élément bouton dans une grille donnée en paramètre
        /// </summary>
        /// <param name="grille">La grille dans laquelle ajouter</param>
        /// <param name="bouton">Le bouton à ajouter</param>
        /// <param name="x">Coordonnée x</param>
        /// <param name="y">Coordonnée y</param>
        private void SetInGrid(Grid grille, Button bouton, int x, int y)
        {
            Grid.SetColumn(bouton, x);
            Grid.SetRow(bouton, y);
            Grille.Children.Add(bouton);
        }

        /// <summary>
        /// Evenement appelé lorsque l'on clique sur une case, permettant de changer la couleur de la case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onButtonClick(object sender, RoutedEventArgs e) 
        {
            if(sender is Button button)
            {
                int x = Grid.GetColumn(button);
                int y = Grid.GetRow(button);

                if (this.rectangles[x, y].Fill == Brushes.White)
                {
                    this.rectangles[x, y].Fill = Brushes.Black;
                }
                else
                {
                    this.rectangles[x, y].Fill = Brushes.White;
                }
                
            }
        }

        /// <summary>
        /// Ne fonctionne pas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.E)
            {
                Jeu jeu = new Jeu(this.rectangles);
                jeu.Jouer();
            }
        }
    }
}
