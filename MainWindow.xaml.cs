using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_of_life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributes
        
        private bool randomPattern;
        
        #endregion
        
        #region Constructor
        
        public MainWindow()
        {
            this.randomPattern = false;
            InitializeComponent();
        }
        
        #endregion
        
        #region Methods

        
        /// <summary>
        /// Event lié au changement de valeur du slider
        /// </summary>
        /// <param name="sender">Element qui actionne l'évènement</param>
        /// <param name="e">J'en ai aucune idée de ce que c'est je l'utilise pas</param>
        private void SliderChangedValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.tailleLabel.Content = $"Taille : {sizeCursor.Value}x{sizeCursor.Value}" ;
        }

        /// <summary>
        /// Event lié lors de l'appui sur le bouton
        /// </summary>
        /// <param name="sender">Element qui actionne l'évènement</param>
        /// <param name="e">J'en ai aucune idée de ce que c'est je l'utilise pas</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game(Convert.ToInt32(sizeCursor.Value),this.randomPattern);
            game.Show();
        }

        /// <summary>
        /// Lorsque la check box est activé
        /// </summary>
        /// <param name="sender">Element qui actionne l'évènement</param>
        /// <param name="e">J'en ai aucune idée de ce que c'est je l'utilise pas</param>
        private void CheckedBox(object sender, RoutedEventArgs e)
        {
            this.randomPattern = true;
        }

        /// <summary>
        /// Lorsque la check box est désactivé
        /// </summary>
        /// <param name="sender">Element qui actionne l'évènement</param>
        /// <param name="e">J'en ai aucune idée de ce que c'est je l'utilise pas</param>
        private void UncheckedBox(object sender, RoutedEventArgs e)
        {
            this.randomPattern = false;
        }
        
        #endregion
    }

}
