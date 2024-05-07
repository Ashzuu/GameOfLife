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
        private bool randomPattern;
        public MainWindow()
        {
            this.randomPattern = false;
            InitializeComponent();
        }

        private void SliderChangedValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.tailleLabel.Content = $"Taille : {sizeCursor.Value}x{sizeCursor.Value}" ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game(Convert.ToInt32(sizeCursor.Value));
            game.DefineCheckBox(this.randomPattern);
            game.Show();
        }

        private void CheckedBox(object sender, RoutedEventArgs e)
        {
            this.randomPattern = true;
        }

        private void UncheckedBox(object sender, RoutedEventArgs e)
        {
            this.randomPattern = false;
        }
    }

}
