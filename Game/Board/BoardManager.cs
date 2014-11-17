using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sorry
{
public class BoardManager{

        public ReadOnlyDictionary<int, GamePiece> StandardSpaces { get; private set; }
        public ReadOnlyDictionary<int, GamePiece> SafetyZone { get; private set; }

        private Dictionary<int, GamePiece> _standardSpaces;
        private Dictionary<int, GamePiece> _safetyZone;

        public BoardManager()
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


        public void PlacePiece(GamePiece gamePiece, SpaceType spaceType)
        {
            PieceLocation currentPieceLocation = GetPieceLocation(gamePiece);
            var newPiecePosition = currentPieceLocation.SpaceNumber + 1;
            
            switch (spaceType)
            {
                case SpaceType.Standard:
                {   
                    if (newPiecePosition == 61)
                    {
                        gamePiece.InSafeZone = true;
                        _standardSpaces[currentPieceLocation.SpaceNumber] = null;
                        PlacePiece(gamePiece, SpaceType.SafeZone);
                    }
                    else
                    {
                        _standardSpaces[currentPieceLocation.SpaceNumber] = null;
                        _standardSpaces[newPiecePosition] = gamePiece;
                    }
                    break;
                }
                case SpaceType.SafeZone:
                {
                    if (currentPieceLocation.SpaceNumber > 0)
                    {
                        _safetyZone[currentPieceLocation.SpaceNumber] = null;
                    }
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

    public struct PieceLocation
    {
        public SpaceType SpaceType { get; private set; }
        public int SpaceNumber { get; private set; }

        public PieceLocation(SpaceType spaceType, int spaceNumber)
            : this()
        {
            SpaceType = spaceType;
            SpaceNumber = spaceNumber;
        }
    }

}
