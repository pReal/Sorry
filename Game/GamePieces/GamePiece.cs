namespace Sorry
{
    public class GamePiece
    {
        public bool InStartSpace { get; set; }
        public bool InSafeZone { get; set; }

        public GamePiece()
        {
            InStartSpace = true;
            InSafeZone = false;
        }

    }
}
