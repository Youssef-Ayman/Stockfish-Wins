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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessGame_PI_FinalProject
{
    /// <summary>
    /// Interaction logic for PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Page
    {
        private bool AgainstComputer;
        public PlayPage(bool AgainstComputer)
        {
            this.AgainstComputer = AgainstComputer;
            InitializeComponent();
            if (AgainstComputer)
            {
                textbox2.Text = "Stockfish";
                textbox2.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(textbox1.Text, textbox2.Text, AgainstComputer);
            if (AgainstComputer)
            {
                main.undo.IsEnabled = false;
                main.redo.IsEnabled = false;
            }
            main.Show();
        }
    }
}
