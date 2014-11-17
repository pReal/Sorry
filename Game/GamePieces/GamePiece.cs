using Game.Players;

namespace Game
{
    public class GamePiece
    {
        public bool InStartSpace { get; set; }
        public bool InSafeZone { get; set; }

        public GamePiece(Player player)
        {
            InStartSpace = true;
            InSafeZone = false;
        }

    }
}
