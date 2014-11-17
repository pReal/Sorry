using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
public class Board{

        public ReadOnlyDictionary<int, GamePiece> StandardSpaces { get; private set; }
        public ReadOnlyDictionary<int, GamePiece> SafetyZone { get; private set; }

        private Dictionary<int, GamePiece> _standardSpaces;
        private Dictionary<int, GamePiece> _safetyZone;

        public Board()
        {
            CreateStandardSpaces();

            CreateSafetyZone();
        }

        private void CreateSafetyZone()
        {
            _safetyZone = new Dictionary<int, GamePiece>();

            for (int i = 1; i <= 5; i++)
            {
                _safetyZone.Add(i, null);
            }

            SafetyZone = new ReadOnlyDictionary<int, GamePiece>(_safetyZone);
        }

        private void CreateStandardSpaces()
        {
            _standardSpaces = new Dictionary<int, GamePiece>();

            for (int i = 1; i <= 60; i++)
            {
                _standardSpaces.Add(i, null);
            }

            StandardSpaces = new ReadOnlyDictionary<int, GamePiece>(_standardSpaces);
        }

        public void RemovePieceFromSpace(int spaceNumber, SpaceType spaceType)
        {
            switch(spaceType)
            {
                case SpaceType.Standard:
                {
                    _standardSpaces[spaceNumber] = null;
                    break;
                }
                case SpaceType.SafeZone:
                {
                    _safetyZone[spaceNumber] = null;
                    break;
                }   
            }
        }

        public void PlacePieceOnSpace(int spaceNumber, SpaceType spaceType, GamePiece gamePiece)
        {
            switch (spaceType)
            {
                case SpaceType.Standard:
                    {
                        _standardSpaces[spaceNumber] = gamePiece;
                        break;
                    }
                case SpaceType.SafeZone:
                    {
                        _safetyZone[spaceNumber] = gamePiece;
                        break;
                    }
            }
        }

        public PieceLocation GetPieceLocation(GamePiece gamePiece)
        {
            
            if(StandardSpaces.Values.Any(x => x == gamePiece))
            {
                var spaceNumber = StandardSpaces.SingleOrDefault(x => x.Value == gamePiece).Key;
                var pieceLocation = new PieceLocation(SpaceType.Standard,spaceNumber);

                return pieceLocation;
            }
            
            if (SafetyZone.Values.Any(x => x == gamePiece))
            {
                var spaceNumber = SafetyZone.SingleOrDefault(x => x.Value == gamePiece).Key;
                var pieceLocation = new PieceLocation(SpaceType.SafeZone, spaceNumber);

                return pieceLocation;
            }

            return new PieceLocation();
        }


        public void MovePiece(GamePiece gamePiece, SpaceType spaceType)
        {
            PieceLocation currentPieceLocation = GetPieceLocation(gamePiece);
            var newPiecePosition = currentPieceLocation.SpaceNumber + 1;
            
            RemovePieceFromSpace(currentPieceLocation.SpaceNumber, currentPieceLocation.SpaceType);
            
            switch (spaceType)
            {
                case SpaceType.Standard:
                {
                    if (currentPieceLocation.SpaceNumber == 60)
                    {
                        MovePiece(gamePiece, SpaceType.SafeZone);
                    }
                    else
                    {           
                        _standardSpaces[newPiecePosition] = gamePiece;
                    }
                    break;
                }
                case SpaceType.SafeZone:
                {
                    _safetyZone[newPiecePosition] = gamePiece;
                    break;
                }
            }   
        }
    }

    public enum SpaceType
    {
        Standard,
        SafeZone
    }

}
