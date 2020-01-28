using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_PI_FinalProject
{
   public class UndoHandler
    {
        public static Move ReverseMove(Move Move1)
        {
            if(Move1.CapturedPiece!=null)
                return new Move(Move1.PreviousPosition, Move1.CurrentPosition,Move1.CapturedPiece);
            return new Move(Move1.PreviousPosition, Move1.CurrentPosition);
        }
    }
}
