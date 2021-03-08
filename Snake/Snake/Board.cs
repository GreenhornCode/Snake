using System;
using System.Collections.Generic;

namespace Snake
{
    class Board
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public char[,] Representation { get; set; }
        public Player Player { get; set; }
        public Food Food { get; set; }
        private readonly Random rand = new Random();
        private List<BoardMember> boardObjects; 
        public Board(int height = 40, int width = 60)
        {
            Height = height;
            Width = width;
            Representation = new char[Height, Width];
            Player = new Player
            {
                LocationX = rand.Next(1, Width - 2),
                LocationY = rand.Next(1, Height - 2),
            };
            Food = new Food
            {
                LocationX = rand.Next(1, Width - 2),
                LocationY = rand.Next(1, Height - 2)
            };
            UpdateMembers();
            Update();
        }
        public IEnumerable<char> IterateBoard()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    yield return Representation[i,j];
                }
            }
        }
        public void Update()
        {
            //updates player location
            Player.Move();
            //checks if food was found
            if (Representation[Player.LocationY, Player.LocationX] == Food.Icon)
            {
                Player.AddPointsToScore();
                Player.Grow();
                Representation[Player.LocationY, Player.LocationX] = Player.Body[0].Icon;
                SetFoodLocation();
            }       
            else if (!(Representation[Player.LocationY, Player.LocationX] == ' ' || Representation[Player.LocationY, Player.LocationX] == Player.Icon))
            {
                Player.StatusValue = Player.Status.Dead;
                Console.Title = "Died to: " + Representation[Player.LocationY, Player.LocationX].ToString();
            }

            //updates blank board
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == 0 || i == Height - 1)
                    {
                        Representation[i, j] = '=';
                    }
                    else
                    {
                        Representation[i, j] = j == 0 || j == (Width - 2) ? '|' : ' ';
                    }
                }
            }
            //draws members to board
            UpdateMembers();
        }

        private void UpdateMembers()
        {
            boardObjects = new List<BoardMember> {Food, Player};
            foreach (BodyPart part in Player.Body)
            {
                boardObjects.Add(part);
            }
            foreach (BoardMember member in boardObjects)
            {
                Representation[member.LocationY, member.LocationX] = member.Icon;
            }
        }

        private void SetFoodLocation()
        {
            Food.LocationX = rand.Next(1, Width - 2);
            Food.LocationY = rand.Next(1, Height - 2);
        }
    }
}