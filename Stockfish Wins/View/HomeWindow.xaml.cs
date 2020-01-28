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

namespace ChessGame_PI_FinalProject
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            this.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/ChessResources/checkered.jpg")));
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; 
        }

        private void Rules(object sender, RoutedEventArgs e)
        {
            Content = new RulesPage(); 
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            Content = new PlayPage(false);
        }
        private void Against_Computer(object sender, RoutedEventArgs e)
        {
            Content = new PlayPage(true);
        }
    }
}
