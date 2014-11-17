using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.Players;

namespace Game
{
    public class GameOfSorry
    {
        public ReadOnlyCollection<Player> Players { get; private set; }

        private readonly Board _gameBoard;

        public GameOfSorry()
        {
            _gameBoard = new Board();
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
        }

        public PieceLocation LocatePlayersGamePiece(Player player)
        {
            return _gameBoard.GetPieceLocation(player.GamePiece);
        }

        private void IncrementPlayersPiece(GamePiece gamePiece)
        {
            if (gamePiece.InSafeZone)
            {
                _gameBoard.MovePiece(gamePiece, SpaceType.SafeZone);
            }
            else
            {
                _gameBoard.MovePiece(gamePiece, SpaceType.Standard);
            }
        }

    }
}



