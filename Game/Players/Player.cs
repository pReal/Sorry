using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game.Players
{
    public class Player
    {
        public GamePiece GamePiece { get; private set; }

        public Player()
        {
            GamePiece = new GamePiece(this);
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
