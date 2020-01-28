using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_PI_FinalProject
{
    public interface ChessEngine
    {
         String GetComputerMove(List<String> MoveOrder);
    } 
    public class StockfishEngine : ChessEngine
    {
        private Process chessEngine = new Process();
        public StockfishEngine()
        {
            StartProcess("stockfish_10_x32.exe");
        }
        public void StartProcess(String processPath)
        {
            chessEngine.StartInfo.FileName = processPath;
            chessEngine.StartInfo.CreateNoWindow = true;
            chessEngine.StartInfo.RedirectStandardInput = true;
            chessEngine.StartInfo.RedirectStandardOutput = true;
            chessEngine.StartInfo.UseShellExecute = false;
        }
        public String GetComputerMove(List<String> MoveOrderInUCINotation)
        {
            chessEngine.Start();
            InputMoveOrder(MoveOrderInUCINotation);
            string processReply2 = chessEngine.StandardOutput.ReadToEnd();
            return MoveExtractor.ExtractMove(processReply2);
        }
        private void InputMoveOrder(List<String> MoveOrderInUCINotation)
        {
            chessEngine.StandardInput.WriteLine("position startpos moves " + String.Join(" ", MoveOrderInUCINotation));
            chessEngine.StandardInput.WriteLine("go");
            System.Threading.Thread.Sleep(300);
            chessEngine.StandardInput.Flush();
            chessEngine.StandardInput.Close();
        }
    }
}
