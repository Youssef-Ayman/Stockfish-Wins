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
    public partial class MainWindow : Window
    {
        private Board chessBoard;
        private Grid[,] VisualBoard = new Grid[8, 8];
        private ChessEngine chessEngine;
        bool AgainstComputer;
        public MainWindow(string name2, string name1,bool AgainstComputer)
        {

            InitializeComponent();

            this.AgainstComputer = AgainstComputer;
            Player1.Content = name1;
            Player2.Content = name2;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            chessBoard = new Board();
            DataContext = chessBoard;
            chessBoard.Changed += ChessBoard_Changed;
            chessBoard.Stalemate += Draw;
            InitializeVisualBoard();
            chessBoard.PlacePieces();

            if (AgainstComputer)
                 chessEngine = new StockfishEngine();
        }
        private void Draw()
        {
            MessageBox.Show("Draw");
            this.Close();
        }
        private void ChessBoard_Changed(object sender, EventArgs e)
        {
            UpdateChessBoard();
            CheckForWinner();
            if (AgainstComputer && !chessBoard.WhiteToMove)
            {
                chessBoard.MakeComputerMove(chessEngine);
            }
            CheckForWinner();
        }
        private void CheckForWinner()
        {
            if (chessBoard.WhiteKing.Mated)
                BlackWins();
            if (chessBoard.BlackKing.Mated)
                WhiteWins();
        }
        private void UpdateChessBoard()
        {
            RenderPieces();
            foreach (ChessCell cell in chessBoard.logicalBoard)
            {
                if (cell.IsLegalMove)
                {
                    RenderDots(cell);
                }
                else
                {
                    ClearDots(cell);
                }
                if (cell.IsOccupied() && cell.Piece.IsPressed)
                    HighlightSquare(cell);
                else
                    UnhighlightSquare(cell);
            }
        }
        private void BlackWins()
        {
            MessageBox.Show(Player1.Content + " wins!");
            Environment.Exit(0);
        }
        private void WhiteWins()
        {
            MessageBox.Show(Player2.Content + " wins!");
            Environment.Exit(0);
        }
        private void HighlightSquare(ChessCell cell)
        {
            VisualBoard[cell.position.Y,cell.position.X].Background = new SolidColorBrush(Colors.Yellow);
        }
        private void UnhighlightSquare(ChessCell cell)
        {
            VisualBoard[cell.position.Y, cell.position.X].Background = new SolidColorBrush(Colors.Green);
            if ((cell.position.X + cell.position.Y) % 2 == 0)
                VisualBoard[cell.position.Y, cell.position.X].Background = new SolidColorBrush(Colors.Green);
            else
                VisualBoard[cell.position.Y, cell.position.X].Background = new SolidColorBrush(Colors.LightGreen);
        }
        private void ClearDots(ChessCell cell)
        {
            if (cell.IsOccupied())
            {
                VisualBoard[cell.position.Y,cell.position.X].Children.Clear();
                RenderPiece(cell, cell.Piece);
            }
            else
                VisualBoard[cell.position.Y, cell.position.X].Children.Clear();
        }
        private void RenderDots(ChessCell cell)
        {
            Image Dot = new Image();
            Dot.Source = new BitmapImage(new Uri("pack://application:,,,/ChessResources/hiclipart.com.png"));
            Dot.Height = 50;
            Dot.Width = 50;
            VisualBoard[cell.position.Y, cell.position.X].Children.Add(Dot);
        }

        public void InitializeVisualBoard() 
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    VisualBoard[i, j] = new Grid();
                    VisualBoard[i, j].MouseDown += MainWindow_MouseDown;
                    if ((j + i) % 2 == 0)
                    {
                        VisualBoard[i,j].Background = new SolidColorBrush(Colors.Green);
                        UniformGrid.Children.Add(VisualBoard[i, j]);
                    }
                    else
                    {
                        VisualBoard[i, j].Background = new SolidColorBrush(Colors.LightGreen);
                        UniformGrid.Children.Add(VisualBoard[i, j]);
                    }
                }

            }
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var visualCell = (Grid)sender;
            ChessCell logicalCell  = GetLogicalCell(visualCell);
            chessBoard.GotClicked(logicalCell);
        }

        public void RenderPieces()
        {
            foreach(ChessCell cell in chessBoard.logicalBoard)
            {
                if (cell.IsOccupied())
                    RenderPiece(cell, cell.Piece);
            }
        }
        public void RenderPiece(ChessCell cell,ChessPiece piece)
        {
            Grid square = VisualBoard[cell.position.Y, cell.position.X];
            square.Children.Clear();
            square.Children.Add(GetChessImage(piece));
            cell.Piece.position = cell.position;
        }
        public ChessCell GetLogicalCell(Grid VisualCell)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (VisualBoard[i,j] == VisualCell)
                        return chessBoard.logicalBoard[i, j];
                }
            }
            throw new Exception("Logical Cell Not Found");
        }
        private Image GetChessImage(ChessPiece Piece)
        {
            Image Mole = new Image();
            Mole.Source = new BitmapImage(new Uri(Set_PNG(Piece)));
            return Mole;
        }
        private string Set_PNG(ChessPiece Piece)
        {
             return ("pack://application:,,,/ChessResources/" + (Piece.IsWhite ? "White" : "Black") + Piece.Type.ToString() + ".png");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            chessBoard.RedoLastMove();
            chessBoard.ResetAllLegalCells();
            chessBoard.UnpressChessCells();
            UpdateChessBoard();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chessBoard.UndoLastMove();
            chessBoard.ResetAllLegalCells();
            chessBoard.UnpressChessCells();
            if (chessBoard.MoveOrderInHumanNotation.Count > 0)
                chessBoard.MoveOrderInHumanNotation.RemoveAt(chessBoard.MoveOrderInHumanNotation.Count - 1);
            UpdateChessBoard();
        }
    }
}
