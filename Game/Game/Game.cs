using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Sorry.Players;

namespace Sorry
{
    public class Game
    {
        public ReadOnlyCollection<Player> Players { get; private set; }
        public Player Winner { get; private set; }


        private readonly BoardManager _boardManager;

        public Game()
        {
            _boardManager = new BoardManager();
        }

        public void InitializePlayers(IEnumerable<Player> players)
        {
            Players = new ReadOnlyCollection<Player>(players.ToList());
        }

        public void MovePlayer(Player player)
        {
            if (player.GamePiece.InStartSpace)
            {
                player.GamePiece.InStartSpace = false;
            }

            IncrementPlayersPiece(player.GamePiece);

            CheckForWin(player);
        }

        public PieceLocation LocatePlayersGamePiece(Player player)
        {
            return _boardManager.GetPieceLocation(player.GamePiece);
        }

        private void IncrementPlayersPiece(GamePiece gamePiece)
        {
            if (gamePiece.InSafeZone)
            {
                _boardManager.PlacePiece(gamePiece, SpaceType.SafeZone);
            }
            else
            {
                _boardManager.PlacePiece(gamePiece, SpaceType.Standard);
            }
        }

        private void CheckForWin(Player player)
        {
            if (player.GamePiece.InHome)
            {
                Winner = player;
            }
        }
    }
}



