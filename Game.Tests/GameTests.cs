using System.Collections.Generic;
using System.Linq;
using Sorry;
using Sorry.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTests.cs
{
    [TestClass]
    public class GameTest
    {
        private Game _game;

        [TestInitialize]
        public void Initialize()
        {
            _game = new Game();
            var players = new List<Player>() { new Player() };

            _game.InitializePlayers(players);
        }

        [TestMethod]
        public void Game_Initialized_PlayerHasPieceInStartSpace()
        {
            Assert.IsTrue(_game.Players.First().GamePiece.InStartSpace);
        }

        [TestMethod]
        public void MovePlayer_GamePiece_InStartSpaceTrue()
        {
            var player = _game.Players.First();

            Assert.IsTrue(player.GamePiece.InStartSpace);

            _game.MovePlayer(player);

            Assert.IsFalse(player.GamePiece.InStartSpace);
        }

        [TestMethod]
        public void MovePlayer_CalledOnceWhilePieceIsInStartSpace_PieceIsIn1stSpaceOnGameBoard()
        {
            var player = _game.Players.First();

            Assert.IsTrue(player.GamePiece.InStartSpace);

            _game.MovePlayer(player);
            Assert.IsFalse(player.GamePiece.InStartSpace);

            PieceLocation pieceLocation = _game.LocatePlayersGamePiece(player);

            Assert.AreEqual(1, pieceLocation.SpaceNumber);
            Assert.AreEqual(SpaceType.Standard, pieceLocation.SpaceType);

        }

        [TestMethod]
        public void MovePlayer_Called3Times_PieceInStartSpace_PieceIsIn2ndSpaceOnGameBoard()
        {
            var player = _game.Players.First();

            Assert.IsTrue(player.GamePiece.InStartSpace);

            MovePlayer(player, 2);

            PieceLocation pieceLocation = _game.LocatePlayersGamePiece(player);

            Assert.AreEqual(2, pieceLocation.SpaceNumber);
            Assert.AreEqual(SpaceType.Standard, pieceLocation.SpaceType);
        }

        [TestMethod]
        public void MovePlayer_Called61Times_PieceInFirstSpaceOfSafeZone()
        {
            var player = _game.Players.First();

            Assert.IsTrue(player.GamePiece.InStartSpace);

            MovePlayer(player, 61);

            PieceLocation pieceLocation = _game.LocatePlayersGamePiece(player);

            Assert.AreEqual(1, pieceLocation.SpaceNumber);
            Assert.AreEqual(SpaceType.SafeZone, pieceLocation.SpaceType);
        }

        [TestMethod]
        public void MovePlayer_Called62Times_PieceInSecondSpaceSafeZone()
        {
            var player = _game.Players.First();

            MovePlayer(player, 62);

            PieceLocation pieceLocation = _game.LocatePlayersGamePiece(player);

            Assert.AreEqual(2, pieceLocation.SpaceNumber);
            Assert.AreEqual(SpaceType.SafeZone, pieceLocation.SpaceType);
        }

        [TestMethod]
        public void MovePlayer_called66Times_PieceAtHome_EqualsTrue()
        {
            var player = _game.Players.First();

            MovePlayer(player, 66);

            Assert.IsTrue(player.GamePiece.InHome);
        }

        [TestMethod]
        public void MovePlayer_EntersHome_PlayerIsWinner()
        {
            var player = _game.Players.First();

            MovePlayer(player, 66);

            Assert.AreEqual(player, _game.Winner);
        }

        private void MovePlayer(Player player, int numOfMoves)
        {
            for (int i = 1; i <= numOfMoves; i++)
            {
                _game.MovePlayer(player);
            }
        }


    }

}
