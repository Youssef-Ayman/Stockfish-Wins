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
using System.Collections.ObjectModel;

namespace ChessGame_PI_FinalProject
{

    public class ChessCell
    {
        public ChessCell()
        {
            position = new Position();
        }
        public ChessCell(Position position)
        {
            this.position = position;
            IsLegalMove = false;
    }
        private bool isLegalMove { get; set; }
        public bool IsLegalMove
        {
            get
            {
                return isLegalMove;
            }
            set
            {
                isLegalMove = value;
            }
        }
        public Position position { get; set; }
        private ChessPiece piece { get; set; }
        public ChessPiece Piece
        {
            get { return piece; }
            set
            {
                piece = value;
                if (piece != null)
                {
                    piece.position = new Position(this.position.X, this.position.Y);
                }
            }
        }
        public bool IsOccupied()
        {
            return piece!=null;
        }
        public bool IsEmpty()
        {
            return !IsOccupied() && !IsLegalMove;
        }
        public void PressPiece()
        {
            if (IsOccupied())
                Piece.IsPressed = true;
        }
    }
}
