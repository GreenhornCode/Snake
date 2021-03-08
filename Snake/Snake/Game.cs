using System;
namespace Snake
{
    class Game
    {
        public Board ActiveBoard { get; set; }
        public Game(int height = 20, int width = 40)
        {
            ActiveBoard = new Board(height, width);
        }

        public void Play(ConsoleKey keypress)
        {
            switch (keypress)
            {
                case ConsoleKey.UpArrow:
                    //checks to stop movement backward on self
                    if (!(ActiveBoard.Player.MovementDirection == Player.Direction.Down))
                    {
                        ActiveBoard.Player.MovementDirection = Player.Direction.Up;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (!(ActiveBoard.Player.MovementDirection == Player.Direction.Up))
                    {
                        ActiveBoard.Player.MovementDirection = Player.Direction.Down;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (!(ActiveBoard.Player.MovementDirection == Player.Direction.Right))
                    {
                        ActiveBoard.Player.MovementDirection = Player.Direction.Left;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (!(ActiveBoard.Player.MovementDirection == Player.Direction.Left))
                    {
                        ActiveBoard.Player.MovementDirection = Player.Direction.Right;
                    }
                    break;
            }
        }
    }
}
