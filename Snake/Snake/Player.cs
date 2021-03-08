using System.Collections.Generic;

namespace Snake
{
    class Player : BoardMember
    {
        public int Score { get; set; }
        public Status StatusValue { get; set; }
        public Direction MovementDirection { get; set; }
        public List<BodyPart> Body { get; set; }
        public Player()
        {
            Icon = 'O';
            StatusValue = Status.Alive;
            Body = new List<BodyPart>();
            MovementDirection = Direction.None;
        }
        public void AddPointsToScore()
        {
            Score += 100;
        }
        public void Grow()
        {
            BodyPart newPart;
            if (Body.Count > 0)
            {
                newPart = new BodyPart(Body[0].LocationX, Body[0].LocationY);
            }
            else
            {
                newPart = new BodyPart(LocationX, LocationY);
            }
            Body.Add(newPart);
        }
        private void ShiftBody()
        {
            for (int i = 0; i < Body.Count; i++)
            {
                Body[i].PreviousX = Body[i].LocationX;
                Body[i].PreviousY = Body[i].LocationY;
                if (i == 0)
                {
                    switch (MovementDirection)
                    {
                        case Direction.Up:
                            Body[i].LocationX = LocationX;
                            Body[i].LocationY = LocationY + 1;
                            break;
                        case Direction.Down:
                            Body[i].LocationX = LocationX;
                            Body[i].LocationY = LocationY - 1;
                            break;
                        case Direction.Left:
                            Body[i].LocationX = LocationX + 1;
                            Body[i].LocationY = LocationY;
                            break;
                        case Direction.Right:
                            Body[i].LocationX = LocationX - 1;
                            Body[i].LocationY = LocationY;
                            break;
                    }
                }
                else
                {
                    Body[i].LocationX = Body[i - 1].PreviousX;
                    Body[i].LocationY = Body[i - 1].PreviousY;
                }
            }
        }
        public void Move()
        {
            switch (MovementDirection)
            {
                case Direction.Up:
                    LocationY -= 1;
                    break;
                case Direction.Down:
                    LocationY += 1;
                    break;
                case Direction.Left:
                    LocationX -= 1;
                    break;
                case Direction.Right:
                    LocationX += 1;
                    break;
            }
            ShiftBody();
        }
        public enum Direction
        {
            None = -1,
            Up,
            Down,
            Left,
            Right
        }
        public enum Status
        {
            Dead,
            Alive
        }
    }
}
