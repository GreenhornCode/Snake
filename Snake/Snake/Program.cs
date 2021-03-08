using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static Stopwatch sw = new Stopwatch();
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);
        static void Main(string[] args)
        {
            Task.Run(() => ShowWindow(Process.GetCurrentProcess().MainWindowHandle, 3));
            int delay;
            Console.Title = "Snake";
            List<int> sessionScores = new List<int>();
            Console.WriteLine("Use your arrow keys to move the snake and eat the @pples to grow larger.\nPress the spacebar to exit, or any other key to start.");
            //game repeat loop
            while (Console.ReadKey().Key != ConsoleKey.Spacebar)
            {
                Console.Clear();
                //write Hiscores
                Console.SetCursorPosition(50, 0);
                Console.Write("  TOP 10  ");
                Console.SetCursorPosition(50, 1);
                Console.WriteLine("----------");
                for (int i = 0; i < sessionScores.Count && i < 10; i++)
                {
                    Console.SetCursorPosition(50, 2 + i);
                    Console.Write(sessionScores[i]);
                }
                Console.SetCursorPosition(0, 0);
                Game game = new Game();
                WriteBoard(game.ActiveBoard);
                game.Play(Console.ReadKey().Key);
                //main game loop
                do
                {
                    Console.CursorVisible = false;
                    //Keep ratio of speed perception relatively the same latitudinally/longitudinally
                    delay = game.ActiveBoard.Player.MovementDirection == Player.Direction.Left ||
                            game.ActiveBoard.Player.MovementDirection == Player.Direction.Right ?
                            30 : 55;
                    DateTime clock = DateTime.Now.AddMilliseconds(delay);
                    while (clock > DateTime.Now)
                    {
                        Console.SetCursorPosition(0, 0);
                        WriteBoard(game.ActiveBoard);
                        if (Console.KeyAvailable)
                        {
                            game.Play(Console.ReadKey(true).Key);
                            break; //prevents player direction from updating and reading another key before movement logic. 
                        }
                    }
                    game.ActiveBoard.Update();
                    Console.WriteLine($"Score: {game.ActiveBoard.Player.Score}");
                }
                while (game.ActiveBoard.Player.StatusValue == Player.Status.Alive);
                sessionScores.Add(game.ActiveBoard.Player.Score);
                sessionScores.Sort((a, b) => b.CompareTo(a));
                Console.WriteLine("Press 'spacebar' to exit, or any other key to play again.");
            }
        }

        static void WriteBoard(Board b)
        {
            //fps timer
            sw.Start();
            int _count = 0;
            foreach (char c in b.IterateBoard())
            {
                _count++;
                if (_count == b.Width)
                {
                    Console.WriteLine();
                    _count = 0;
                }
                else
                {
                    Console.Write(c);
                }
            }
            sw.Stop();
            Console.Title = $"Snake - FPS: {1000 / sw.ElapsedMilliseconds}"; //measure of draw speed, not game speed
            sw.Reset();
        }
    }
}