namespace Sorry.Players
{
    public class Player
    {
        public GamePiece GamePiece { get; private set; }

        public Player()
        {
            GamePiece = new GamePiece();
        }
    }

    public enum PlayerColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }
}
