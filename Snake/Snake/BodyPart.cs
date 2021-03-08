namespace Snake
{
    class BodyPart : BoardMember
    {
        public int PreviousX { get; set; }
        public int PreviousY { get; set; }
        public BodyPart(int x, int y)
        {
            LocationX = x;
            LocationY = y;
            Icon = '#';
        }
    }
}